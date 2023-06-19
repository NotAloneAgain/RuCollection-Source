using Exiled.API.Features;
using System.Linq;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using PlayerEvent = Exiled.Events.Handlers.Player;
using Utils.NonAllocLINQ;

namespace MiscPlugins.RemoteKeycard.Handlers
{
    internal sealed class EventHandlers
    {

        public EventHandlers()
        {
            PlayerEvent.InteractingDoor += OnInteractingDoor;
            PlayerEvent.UnlockingGenerator += OnUnlockingGenerator;
            PlayerEvent.ActivatingWarheadPanel += OnActivatingWarheadPanel;
            PlayerEvent.InteractingLocker += OnInteractingLocker;
        }
        public void UnsubscribeEvents()
        {
            PlayerEvent.InteractingDoor -= OnInteractingDoor;
            PlayerEvent.UnlockingGenerator -= OnUnlockingGenerator;
            PlayerEvent.ActivatingWarheadPanel -= OnActivatingWarheadPanel;
            PlayerEvent.InteractingLocker -= OnInteractingLocker;
        }
        private void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (!ev.IsAllowed && HasKeycardPermission(ev.Player, ev.Door.RequiredPermissions.RequiredPermissions))
                ev.IsAllowed = true;
        }
        private void OnUnlockingGenerator(UnlockingGeneratorEventArgs ev)
        {
            if (!ev.IsAllowed && HasKeycardPermission(ev.Player, ev.Generator.Base._requiredPermission))
                ev.IsAllowed = true;
        }
        private void OnInteractingLocker(InteractingLockerEventArgs ev)
        {
            if (!ev.IsAllowed && ev.Chamber != null && HasKeycardPermission(ev.Player, ev.Chamber.RequiredPermissions))
            {
                ev.Chamber.GetType();
                ev.IsAllowed = true;
            }
        }
        private void OnActivatingWarheadPanel(ActivatingWarheadPanelEventArgs ev)
        {
            if (!ev.IsAllowed && HasKeycardPermission(ev.Player, Interactables.Interobjects.DoorUtils.KeycardPermissions.AlphaWarhead))
                ev.IsAllowed = true;
        }
        bool HasKeycardPermission(Player player, Interactables.Interobjects.DoorUtils.KeycardPermissions permissions)
        {
            return player.Items.Any(item => item is Keycard keycard && (keycard.Base.Permissions & permissions) != 0);
        }
    }
}
