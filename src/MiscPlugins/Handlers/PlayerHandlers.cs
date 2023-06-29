using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Player;
using RuCollection.API;
using RuCollection.API.Subclasses.Single;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MiscPlugins.Handlers
{
    internal sealed class PlayerHandlers
    {
        private readonly string _weaponHintText;
        private readonly Dictionary<Player, System.DateTime> _painkillersUsed;

        private int _activated = 0;

        public PlayerHandlers(string weaponHintText)
        {
            _weaponHintText = weaponHintText;
            _painkillersUsed = new(50);
        }

        public void OnInteractingDoor(InteractingDoorEventArgs ev) => ev.IsAllowed
                = ev.IsAllowed || ev.Player.Items.Any(x => x.Is(out Keycard card) && ev.Door.RequiredPermissions.CheckPermissions(card.Base, ev.Player.ReferenceHub));

        public void OnInteractingLocker(InteractingLockerEventArgs ev) => ev.IsAllowed
                = ev.IsAllowed || ev.Player.Items.Any(x => x.Is(out Keycard card) && HasFlagFast(card.Permissions, KeycardPermissions.Checkpoints) && HasFlagFast(card.Permissions, KeycardPermissions.ContainmentLevelTwo));

        public void OnUnlockingGenerator(UnlockingGeneratorEventArgs ev) => ev.IsAllowed
                = ev.IsAllowed || ev.Player.Items.Any(x => x.Is(out Keycard card) && HasFlagFast(ev.Generator.KeycardPermissions, KeycardPermissions.ArmoryLevelTwo));

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (!ev.IsAllowed || ev.NewRole.GetTeam() == PlayerRoles.Team.SCPs)
            {
                return;
            }

            if (ev.NewRole.GetTeam() == PlayerRoles.Team.ChaosInsurgency && Random.Range(0, 101) >= 48)
            {
                ev.Items.Add(Random.Range(0, 101) switch
                {
                    >= 50 => ItemType.GrenadeHE,
                    _ => ItemType.GrenadeFlash
                });
            }

            ev.Ammo.Clear();

            ev.Ammo.Add(ItemType.Ammo12gauge, 200);
            ev.Ammo.Add(ItemType.Ammo44cal, 200);
            ev.Ammo.Add(ItemType.Ammo556x45, 200);
            ev.Ammo.Add(ItemType.Ammo762x39, 200);
            ev.Ammo.Add(ItemType.Ammo9x19, 200);
        }

        public void OnReloadingWeapon(ReloadingWeaponEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Firearm.Type is ItemType.ParticleDisruptor or ItemType.MicroHID || ev.Firearm == null)
            {
                return;
            }

            ev.Player.AddAmmo(ev.Firearm.AmmoType, ev.Firearm.MaxAmmo);
        }

        public void OnShot(ShotEventArgs ev)
        {
            if (ev.Player.CurrentItem.Type is ItemType.ParticleDisruptor or ItemType.GunShotgun || !ev.CanHurt || ev.Target == null || ev.Player.LeadingTeam == ev.Target.LeadingTeam || ev.Player.IsScp)
            {
                return;
            }

            var weapon = ev.Player.CurrentItem.As<Firearm>();

            if (weapon == null || weapon.Ammo >= Mathf.RoundToInt(weapon.MaxAmmo * 0.66f))
            {
                return;
            }

            if (Random.Range(0, 101) == 100)
            {
                ev.Player.CurrentItem.As<Firearm>().Ammo = 0;

                ev.Player.ShowHint(_weaponHintText, 8);
            }
        }

        public void OnHurting(HurtingEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player == ev.Attacker || ev.Attacker == null || !ev.Player.IsHuman)
            {
                return;
            }

            if (ev.DamageHandler.Type == DamageType.Scp939 || ev.DamageHandler.Type == DamageType.Firearm && !ev.Player.HasItem(ItemType.ArmorHeavy))
            {
                ev.Player.EnableEffect(EffectType.Bleeding, 4, true);
            }

            if (_painkillersUsed.TryGetValue(ev.Player, out var time) && (System.DateTime.Now - time).TotalSeconds <= 120 && ev.DamageHandler.Type is DamageType.Falldown or DamageType.Bleeding or DamageType.Explosion or DamageType.Scp or DamageType.Scp018 or DamageType.Scp207 or DamageType.Firearm)
            {
                ev.Amount *= 0.9f;
            }
        }

        public void OnDying(DyingEventArgs ev)
        {
            if (ev.Attacker == null || !ev.Player.IsHuman || !ev.IsAllowed || ev.Player == ev.Attacker || ev.Attacker.Role.Type != PlayerRoles.RoleTypeId.Scp0492)
            {
                return;
            }

            if (Random.Range(0, 101) >= 90)
            {
                ev.IsAllowed = false;

                ev.Player.DropAllWithoutKeycard();

                ev.Player.Role.Set(PlayerRoles.RoleTypeId.Scp0492, SpawnReason.Died, PlayerRoles.RoleSpawnFlags.None);
            }
        }

        public void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player.Role.Team != PlayerRoles.Team.FoundationForces && ev.Player != Manager.Singleton.Player)
            {
                return;
            }

            ev.IsAllowed = false;
            ev.IsInIdleRange = false;
            ev.IsInHurtingRange = false;
            ev.IsTriggerable = false;
        }

        public void OnUsedItem(UsedItemEventArgs ev)
        {
            if (ev.Item.Type == ItemType.Painkillers)
            {
                if (!_painkillersUsed.ContainsKey(ev.Player))
                {
                    _painkillersUsed.Add(ev.Player, System.DateTime.Now);
                }
                else
                {
                    _painkillersUsed[ev.Player] = System.DateTime.Now;
                }
            }

            if (ev.Item.Type == ItemType.SCP500)
            {
                ev.Player.DisableAllEffects();
            }
        }

        public void OnActivatingGenerator(ActivatingGeneratorEventArgs ev)
        {
            var computers = Player.Get(PlayerRoles.RoleTypeId.Scp079);

            if (ev.Player.LeadingTeam == LeadingTeam.Anomalies)
            {
                ev.IsAllowed = false;
                ev.Generator.IsActivating = false;

                return;
            }

            if (!ev.IsAllowed || computers.Count() == 0)
            {
                return;
            }

            _activated++;

            foreach (var ply in computers)
            {
                ply.Role.As<Scp079Role>().AddExperience(40 * _activated, PlayerRoles.PlayableScps.Scp079.Scp079HudTranslation.YouAreBeingAttacked);
            }
        }

        private static bool HasFlagFast(KeycardPermissions en1, KeycardPermissions en2) => (en1 & en2) == en2;
    }
}
