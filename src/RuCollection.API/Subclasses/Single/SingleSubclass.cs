using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace RuCollection.API.Subclasses.Single
{
    public abstract class SingleSubclass : SubclassBase
    {
        public Player Player { get; private set; }

        public override void Assign(Player player)
        {
            if (player == null || Player != null)
            {
                return;
            }

            base.Assign(player);

            Player = player;
        }

        public override void Deassign(Player player)
        {
            if (Player == null || Player != player)
            {
                return;
            }

            base.Deassign(player);

            Player = null;
        }

        protected override void OnPlayerLeft(LeftEventArgs ev)
        {
            if (Player != ev.Player)
            {
                return;
            }

            Destroy(ev.Player);
        }

        protected override void OnDied(DiedEventArgs ev)
        {
            if (Player != ev.Player)
            {
                return;
            }

            Destroy(ev.Player);
        }
    }
}
