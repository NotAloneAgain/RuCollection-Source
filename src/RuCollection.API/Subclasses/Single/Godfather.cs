using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using RuCollection.API.Subclasses.Single;

namespace RuCollection.API.Subclasses.Group
{
    public sealed class Godfather : SingleSubclass
    {
        private static Godfather _singleton;

        private Godfather() { }

        public static Godfather Singleton => _singleton ??= new();

        public override string Name { get; } = "Незаконно осужденный";

        public override RoleTypeId Role { get; } = RoleTypeId.ClassD;

        public override string Message { get; } = "Вы были незаконно осуждены и отправлены в комплекс.\nБог сочувствует вам, из-за этого вы получаете на 15% меньше урона, но и наносите людям на 10% меньше, SCP на 5%.";

        public override void Assign(Player player)
        {
            base.Assign(player);

            player.GetEffect(EffectType.DamageReduction)?.ServerSetState(30, 0, false);
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
            if (ev.Attacker != Player && ev.Player != Player || !ev.IsAllowed || ev.DamageHandler.Type is DamageType.Unknown or DamageType.Falldown or DamageType.Warhead or DamageType.Decontamination or DamageType.Recontainment or DamageType.Crushed or DamageType.FemurBreaker or DamageType.PocketDimension or DamageType.SeveredHands)
            {
                return;
            }

            if (ev.Attacker == Player)
            {
                ev.Amount *= ev.Player.Role.Side switch
                {
                    Side.Scp => 0.95f,
                    _ => 0.9f
                };

                return;
            }
        }
    }
}
