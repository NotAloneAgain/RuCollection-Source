﻿using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using HarmonyLib;
using InventorySystem.Items.Firearms.Ammo;
using MEC;
using PlayerEvent = Exiled.Events.Handlers.Player;

namespace MiscPlugins.UnlimitedAmmo.Handlers
{
    internal sealed class EventHandlers
    {
        public EventHandlers()
        {
            PlayerEvent.Handcuffing += OnHandcuffing;
            PlayerEvent.RemovingHandcuffs += OnRemovingHandcuffs;
            PlayerEvent.DroppingAmmo += OnDroppingAmmo;
            PlayerEvent.Dying += OnDying;
            PlayerEvent.ReloadingWeapon += OnReloadWeapon;
            PlayerEvent.ChangingRole += OnChangingRole;
            Exiled.Events.Handlers.Map.SpawningItem += OnSpawningItem;
        }
        public void UnsubscribeEvents()
        {
            PlayerEvent.Handcuffing -= OnHandcuffing;
            PlayerEvent.RemovingHandcuffs -= OnRemovingHandcuffs;
            PlayerEvent.DroppingAmmo -= OnDroppingAmmo;
            PlayerEvent.Dying -= OnDying;
            PlayerEvent.ReloadingWeapon -= OnReloadWeapon;
            PlayerEvent.ChangingRole -= OnChangingRole;
            Exiled.Events.Handlers.Map.SpawningItem -= OnSpawningItem;
        }
        private void OnSpawningItem(SpawningItemEventArgs ev)
        {
            if(ev.Pickup.Base is AmmoPickup)
            {
                ev.IsAllowed = false;
            }
        }
        private void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player.IsScp) return;

            //if (CustomRole.Get((uint)2).Check(ev.Player)) return;

            ev.Player.SetAmmo(AmmoType.Nato9, 0);
            ev.Player.SetAmmo(AmmoType.Ammo44Cal, 0);
            ev.Player.SetAmmo(AmmoType.Nato762, 0);
            ev.Player.SetAmmo(AmmoType.Ammo12Gauge, 0);
            ev.Player.SetAmmo(AmmoType.Nato556, 0);


            Timing.CallDelayed(0.1f, () =>
            {
                ev.Player.SetAmmo(AmmoType.Nato9, 999);
                ev.Player.SetAmmo(AmmoType.Ammo44Cal, 999);
                ev.Player.SetAmmo(AmmoType.Nato762, 999);
                ev.Player.SetAmmo(AmmoType.Ammo12Gauge, 14);
                ev.Player.SetAmmo(AmmoType.Nato556, 999);
            });
        }
        private void OnReloadWeapon(ReloadingWeaponEventArgs ev)
        {
            //if (CustomRole.Get((uint)2).Check(ev.Player)) return;

            if (ev.Player.IsScp) return;

            Timing.CallDelayed(0.1f, () =>
            {
                ev.Player.SetAmmo(AmmoType.Nato9, 999);
                ev.Player.SetAmmo(AmmoType.Ammo44Cal, 999);
                ev.Player.SetAmmo(AmmoType.Nato762, 999);
                ev.Player.SetAmmo(AmmoType.Ammo12Gauge, 14);
                ev.Player.SetAmmo(AmmoType.Nato556, 999);
            });
        }
        private void OnDroppingAmmo(DroppingAmmoEventArgs ev) => ev.IsAllowed = false;
        private void OnDying(DyingEventArgs ev)
        {
            if (ev.Player == null) return;

            if (ev.Player.IsScp) return;

            ev.Player.SetAmmo(AmmoType.Nato9, 0);
            ev.Player.SetAmmo(AmmoType.Ammo44Cal, 0);
            ev.Player.SetAmmo(AmmoType.Nato762, 0);
            ev.Player.SetAmmo(AmmoType.Ammo12Gauge, 0);
            ev.Player.SetAmmo(AmmoType.Nato556, 0);
        }
        public void OnHandcuffing(HandcuffingEventArgs ev)
        {
            if (ev.Player.IsScp) return;

            ev.Target.SetAmmo(AmmoType.Nato9, 0);
            ev.Target.SetAmmo(AmmoType.Ammo44Cal, 0);
            ev.Target.SetAmmo(AmmoType.Nato762, 0);
            ev.Target.SetAmmo(AmmoType.Ammo12Gauge, 0);
            ev.Target.SetAmmo(AmmoType.Nato556, 0);
        }
        private void OnRemovingHandcuffs(RemovingHandcuffsEventArgs ev)
        {
            if (ev.Player.IsScp) return;

            Timing.CallDelayed(0.1f, () =>
            {
                ev.Player.SetAmmo(AmmoType.Nato9, 999);
                ev.Player.SetAmmo(AmmoType.Ammo44Cal, 999);
                ev.Player.SetAmmo(AmmoType.Nato762, 999);
                ev.Player.SetAmmo(AmmoType.Ammo12Gauge, 14);
                ev.Player.SetAmmo(AmmoType.Nato556, 999);
            });
        }
    }
}