using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using System.Linq;
using UnityEngine;

namespace RuCollection.API.Subclasses.Single
{
    public sealed class Scp181 : SingleSubclass
    {
        private static Scp181 _singleton;

        private Scp181() { }

        public static Scp181 Singleton
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

        public override string Name { get; } = "SCP-181";

        public override RoleTypeId Role { get; } = RoleTypeId.ClassD;

        public override string Message { get; } = "Вы - Везунчик.\nВам очень сильно везет по жизни (проверь консоль).";

        public override string Color { get; } = "#44944A";

        public override bool Show { get; } = true;

        public override Inventory Inventory { get; } = new Inventory(new(1)
        {
            { 0, new (1)
            {
                { ItemType.KeycardJanitor, 100 },
            }
            }
        });

        public override void Assign(Player player)
        {
            base.Assign(player);

            player.GetEffect(EffectType.MovementBoost).ServerSetState(4, 0, false);

            string message = "\n\t\t+ У тебя есть:" +
                "\n\t- Шанс в 4% открыть дверь, к которой доступа не имеешь из-за сбоев в комплексе." +
                "\n\t- На 10% меньше урона от любых источников, ведь все травмы приходятся в менее важные места." +
                "\n\t- Шанс 5% избежать смертельный урон." +
                "\n\t- Увеличение скорости на 4% из-за правильного дыхания и атлетичного телосложения.";

            player.SendConsoleMessage(message, "yellow");
        }

        public override void Subscribe()
        {
            base.Subscribe();

            Exiled.Events.Handlers.Player.Dying += OnDying;
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            Exiled.Events.Handlers.Player.InteractingDoor += OnInteractingDoor;
        }

        public override void Unsubscribe()
        {
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
            Exiled.Events.Handlers.Player.Dying -= OnDying;
            Exiled.Events.Handlers.Player.InteractingDoor -= OnInteractingDoor;

            base.Unsubscribe();
        }

        private void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Player != Player || ev.DamageHandler.Type is DamageType.Unknown or DamageType.Falldown or DamageType.Warhead or DamageType.Decontamination or DamageType.Recontainment or DamageType.Crushed or DamageType.FemurBreaker or DamageType.PocketDimension or DamageType.SeveredHands)
            {
                return;
            }

            ev.Amount *= 0.9f;
        }

        private void OnDying(DyingEventArgs ev)
        {
            if (ev.Player != Player || ev.DamageHandler.Type is DamageType.Unknown or DamageType.Falldown or DamageType.Warhead or DamageType.Decontamination or DamageType.Recontainment or DamageType.Crushed or DamageType.FemurBreaker or DamageType.PocketDimension or DamageType.SeveredHands)
            {
                return;
            }

            if (Random.Range(0, 101) >= 95)
            {
                ev.IsAllowed = false;
                ev.ItemsToDrop.Clear();
            }
        }

        private void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (ev.Player != Player || ev.IsAllowed || ev.Door.IsMoving)
            {
                return;
            }

            if (Random.Range(0, 101) >= 96)
            {
                var otherDoors = ev.Door.Room.Doors.Where(door => door != ev.Door);

                foreach (var door in otherDoors)
                {
                    door.IsOpen = false;
                    door.Lock(1.5f, DoorLockType.NoPower);
                }

                ev.Door.Room.TurnOffLights(1);

                ev.IsAllowed = true;
            }
        }
    }
}
