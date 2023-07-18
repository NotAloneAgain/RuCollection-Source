using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace RuCollection.API.Subclasses.Single
{
    public sealed class Infected : SingleSubclass
    {
        private static Infected _singleton;

        private Infected() { }

        public static Infected Singleton => _singleton ??= new();

        public override string Name { get; } = "Зараженный";

        public override RoleTypeId Role { get; } = RoleTypeId.Scientist;

        public override string Message { get; } = "Вы - зараженный!\nВы пониженное кол-во здоровья и заражение зомби-вирусом. После смерти или по команде .zombie вы обратитесь...";

        public override float Health { get; } = 80;

        public override bool KeepOnEscape { get; } = true;

        public override Inventory Inventory { get; } = new Inventory(new(2)
        {
            { 0, new (1)
            {
                { ItemType.KeycardScientist, 100 },
            }
            },
            { 1, new (1)
            {
                { ItemType.Medkit, 33 },
            }
            },
        });
        public override void Subscribe()
        {
            base.Subscribe();

            Exiled.Events.Handlers.Player.Dying += OnDying;
        }

        public override void Unsubscribe()
        {
            Exiled.Events.Handlers.Player.Dying -= OnDying;

            base.Unsubscribe();
        }

        private void OnDying(DyingEventArgs ev)
        {
            if (ev.Player != Player || !ev.IsAllowed || ev.DamageHandler.Type is DamageType.Unknown or DamageType.Warhead or DamageType.Recontainment or DamageType.Crushed or DamageType.FemurBreaker or DamageType.PocketDimension or DamageType.SeveredHands || Player.Role.Type != Role)
            {
                return;
            }

            ev.IsAllowed = false;
            ev.Player.DropAllWithoutKeycard();

            ev.Player.Role.Set(RoleTypeId.Scp0492, SpawnReason.Revived, RoleSpawnFlags.None);
        }
    }
}
