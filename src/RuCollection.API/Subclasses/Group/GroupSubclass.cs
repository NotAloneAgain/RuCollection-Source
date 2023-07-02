using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using System.Collections.Generic;

namespace RuCollection.API.Subclasses.Group
{
    public abstract class GroupSubclass : SubclassBase
    {
        private readonly int _max;

        public GroupSubclass(int count = 0)
        {
            Players = new (count == 0 ? 60 : count);

            _max = count;;
        }

        public List<Player> Players { get; private set; }

        public override void Assign(Player player)
        {
            if (Players.Count + 1 == _max || player == null)
            {
                return;
            }

            base.Assign(player);

            Players.Add(player);
        }

        public override void Deassign(Player player)
        {
            if (Players.Count == 0 || player == null)
            {
                return;
            }

            base.Deassign(player);

            Players.Remove(player);
        }

        protected override void OnPlayerLeft(LeftEventArgs ev)
        {
            if (!Players.Contains(ev.Player))
            {
                return;
            }

            base.OnPlayerLeft(ev);
        }

        protected override void OnDied(DiedEventArgs ev)
        {
            if (!Players.Contains(ev.Player))
            {
                return;
            }

            base.OnDied(ev);
        }
    }
}
