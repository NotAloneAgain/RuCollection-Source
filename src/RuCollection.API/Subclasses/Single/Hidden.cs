using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using RuCollection.API.Subclasses.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuCollection.API.Subclasses.Single
{
    public sealed class Hidden : SingleSubclass
    {
        private static Hidden _singleton;

        private Hidden() { }

        public static Hidden Singleton
        {
            get
            {
                if (_singleton == null)
                {
                    _singleton = new();
                }

                return _singleton;
            }
        }

        public override string Name { get; } = "Скрытный";

        public override RoleTypeId Role { get; } = RoleTypeId.Scientist;

        public override string Message { get; } = "Вы - скрытный!\nПо команде .hide вы можете стать невидимым на 20 секунд. Перезарядка: 3 минуты.";

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
            if (ev.Player != Player || !ev.IsAllowed || ev.DamageHandler.Type is DamageType.Unknown or DamageType.Warhead or DamageType.Recontainment or DamageType.Crushed or DamageType.FemurBreaker or DamageType.PocketDimension or DamageType.SeveredHands)
            {
                return;
            }

            ev.IsAllowed = false;
            ev.Player.DropAllWithoutKeycard();

            ev.Player.Role.Set(RoleTypeId.Scp0492, SpawnReason.Revived, RoleSpawnFlags.None);
        }
    }
}
