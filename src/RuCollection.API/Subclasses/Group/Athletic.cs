using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using RuCollection.API.Subclasses.Group;
using System.Collections.Generic;

namespace RuCollection.API.Subclasses.Single
{
    public sealed class Athletic : GroupSubclass
    {
        private static Athletic _singleton;
        private Dictionary<Player, bool> _boosted;

        private Athletic() : base(2)
        {
            _boosted = new(10);
        }

        public static Athletic Singleton => _singleton ??= new();

        public override string Name { get; } = "Атлет";

        public override RoleTypeId Role { get; } = RoleTypeId.Scientist;

        public override string Message { get; } = "Вы - атлет!\nВы имеете быстрые ноги, дающие +4% к скорости и закаленное тело, дающее 110 единиц здоровья. Если здоровье будет ниже 55 единиц адреналин даст одноразовый буст к скорости в размере 15% на 10 секунд.";

        public override float Health { get; } = 110;

        public override Inventory Inventory { get; } = new Inventory(new(2)
        {
            { 0, new (1)
            {
                { ItemType.KeycardScientist, 100 },
            }
            },
            { 1, new (1)
            {
                { ItemType.Medkit, 100 },
            }
            },
        });

        public override void Assign(Player player)
        {
            base.Assign(player);

            player.GetEffect(EffectType.MovementBoost).ServerSetState(4, 0, false);

            _boosted.Add(player, false);
        }

        public override void Deassign(Player player)
        {
            base.Deassign(player);

            if (!Players.Contains(player))
            {
                return;
            }

            _boosted.Remove(player);
        }

        public override void Subscribe()
        {
            base.Subscribe();

            Exiled.Events.Handlers.Player.Hurting += OnHurting;
        }

        public override void Unsubscribe()
        {
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;

            base.Unsubscribe();
        }

        private void OnHurting(HurtingEventArgs ev)
        {
            if (!Players.Contains(ev.Player) || !ev.IsAllowed || ev.DamageHandler.Type is DamageType.Unknown or DamageType.Warhead or DamageType.Recontainment or DamageType.Crushed or DamageType.FemurBreaker or DamageType.PocketDimension or DamageType.SeveredHands)
            {
                return;
            }

            if (!_boosted[ev.Player] && (ev.Player.Health < 55 || ev.Player.Health - ev.Amount is < 55 and > 0))
            {
                _boosted[ev.Player] = true;

                var effect = ev.Player.GetEffect(EffectType.MovementBoost);

                effect.ServerSetState(15, 10, false);

                Timing.CallDelayed(10, () => effect.ServerSetState(4, 0));
            }
        }
    }
}
