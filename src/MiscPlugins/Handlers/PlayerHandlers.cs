using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using System;
using System.Linq;

namespace MiscPlugins.Handlers
{
    internal sealed class PlayerHandlers
    {
        public void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (ev.IsAllowed)
            {
                return;
            }

            ev.IsAllowed = ev.Player.Items.Any(x => x.Is(out Keycard card) && ev.Door.RequiredPermissions.CheckPermissions(card.Base, ev.Player.ReferenceHub));
        }

        public void OnInteractingLocker(InteractingLockerEventArgs ev)
        {
            if (ev.IsAllowed)
            {
                return;
            }

            ev.IsAllowed = ev.Player.Items.Any(x => x.Is(out Keycard card) && HasFlagFast(card.Permissions, KeycardPermissions.Checkpoints) && HasFlagFast(card.Permissions, KeycardPermissions.ContainmentLevelTwo));
        }

        public void OnUnlockingGenerator(UnlockingGeneratorEventArgs ev)
        {
            if (ev.IsAllowed)
            {
                return;
            }

            ev.IsAllowed = ev.Player.Items.Any(x => x.Is(out Keycard card) && HasFlagFast(ev.Generator.KeycardPermissions, KeycardPermissions.ArmoryLevelTwo));
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (!ev.IsAllowed || ev.NewRole.GetTeam() == PlayerRoles.Team.SCPs)
            {
                return;
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
            if (!ev.IsAllowed)
            {
                return;
            }

            ev.Player.SetAmmo(ev.Firearm.AmmoType, ev.Firearm.MaxAmmo);
        }

        private static bool HasFlagFast(KeycardPermissions en1, KeycardPermissions en2) => (en1 & en2) == en2;
    }
}
