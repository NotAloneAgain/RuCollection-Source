using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using System.Collections.Generic;
using UnityEngine;

namespace RuCollection.API.Subclasses.Single
{
    public sealed class Scp343 : SingleSubclass
    {
        private static Scp343 _singleton;
        private RoleTypeId _roleTypeId;
        private CoroutineHandle _coroutine;

        private Scp343() { }

        public static Scp343 Singleton => _singleton ??= new();

        public override string Name { get; } = "SCP-343";

        public override RoleTypeId Role { get; } = RoleTypeId.ClassD;

        public override string Message { get; } = "Вы - Бог.\nО своих способностях ты можешь прочитать в консоли.";

        public override string Color { get; } = "#44944A";

        public override bool Show { get; } = true;

        public override Inventory Inventory { get; } = new Inventory(new(1)
        {
            { 0, new (1)
            {
                { ItemType.Coin, 100 },
            }
            }
        });

        public override void Assign(Player player)
        {
            base.Assign(player);

            player.IsGodModeEnabled = true;

            _roleTypeId = RoleTypeId.Tutorial;

            string message = "\n\t\t+ У тебя есть:" +
                "\n\t- Возможность стать невидимым с помощью подбрасывания монетки." +
                "\n\t- Аура исцеления для людей." +
                "\n\t- Смена собственного размера [.size]." +
                "\n\t- Открытия любых дверей кроме оружеек." +
                "\n\t- Преобразование опасных предметов в аптечки путем подбирания." +
                "\n\t- Выдача любых предметов кроме опасных [.item].";

            player.Ammo.Clear();

            player.SendConsoleMessage(message, "yellow");

            Timing.CallDelayed(0.1f, () => player.Role.Set(RoleTypeId.Tutorial, SpawnReason.Respawn, RoleSpawnFlags.None));

            _coroutine = Timing.RunCoroutine(_HealAura());
        }

        public override void Deassign(Player player)
        {
            base.Deassign(player);

            player.IsGodModeEnabled = false;

            Timing.KillCoroutines(_coroutine);
        }

        public override void Subscribe()
        {
            base.Subscribe();

            Exiled.Events.Handlers.Player.FlippingCoin += OnFlippingCoin;
            Exiled.Events.Handlers.Player.PickingUpItem += OnPickupingItem;
            Exiled.Events.Handlers.Player.InteractingDoor += OnInteractingDoor;
        }

        public override void Unsubscribe()
        {
            Exiled.Events.Handlers.Player.FlippingCoin -= OnFlippingCoin;
            Exiled.Events.Handlers.Player.PickingUpItem -= OnPickupingItem;
            Exiled.Events.Handlers.Player.InteractingDoor -= OnInteractingDoor;

            base.Unsubscribe();
        }

        protected override void OnEscaping(EscapingEventArgs ev)
        {
            return;
        }

        private void OnFlippingCoin(FlippingCoinEventArgs ev)
        {
            if (ev.Player != Player)
            {
                return;
            }

            int role = (int)_roleTypeId + 1;

            if (role >= 20)
            {
                role = 0;
            }

            ev.Player.ChangeAppearance((RoleTypeId)role, false);
        }

        private void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (ev.Player != Player || ev.IsAllowed || ev.Door.IsMoving)
            {
                return;
            }

            ev.IsAllowed = ev.Door.Type switch
            {
                DoorType.CheckpointArmoryA or DoorType.CheckpointArmoryB
                or DoorType.HczArmory or DoorType.LczArmory
                or DoorType.Scp049Armory or DoorType.Scp173Armory
                or DoorType.HID => false,
                _ => true,
            };
        }

        private void OnPickupingItem(PickingUpItemEventArgs ev)
        {
            if (ev.Player != Player || !ev.IsAllowed)
            {
                return;
            }

            if (ev.Pickup.Info.ItemId.GetCategory() is ItemCategory.Firearm or ItemCategory.Grenade or ItemCategory.SCPItem or ItemCategory.MicroHID or ItemCategory.Ammo)
            {
                ev.Pickup.Destroy();
                ev.IsAllowed = false;
                ev.Player.AddItem(ItemType.Medkit);
            }
        }

        private IEnumerator<float> _HealAura()
        {
            while (Player != null && Player.IsAlive)
            {
                foreach (Player ply in Player.Get(p => Vector3.Distance(p.Position, Player.Position) <= 5))
                {
                    if (ply == Player)
                        continue;

                    if (ply.Role.Team == Team.SCPs)
                    {
                        ply.Heal(1, false);

                        continue;
                    }

                    ply.Heal(2, false);

                    if (ply.Health == ply.MaxHealth && ply.MaxHealth < 200)
                    {
                        ply.MaxHealth += 2;
                    }
                }

                yield return Timing.WaitForSeconds(1);
            }
        }
    }
}
