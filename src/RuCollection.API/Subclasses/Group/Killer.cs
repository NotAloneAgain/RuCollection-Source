using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using System.Linq;

namespace RuCollection.API.Subclasses.Group
{
    public sealed class Killer : GroupSubclass
    {
        private static Killer _singleton;

        private Killer() : base(2) { }

        public static Killer Singleton => _singleton ??= new();

        public override string Name { get; } = "Убийца";

        public override RoleTypeId Role { get; } = RoleTypeId.ClassD;

        public override string Message { get; } = "Вы - Убийца!\nУ вас есть информацию о пистолете (проверь консоль) и +3% к наносимому вами урону.";

        public override Inventory Inventory { get; } = new Inventory(new(1)
        {
            { 0, new (1)
            {
                { ItemType.Medkit, 100 },
            }
            },
            { 1, new (1)
            {
                { ItemType.Painkillers, 100 },
            }
            },
            { 2, new (1)
            {
                { ItemType.Painkillers, 50 },
            }
            }
        });

        public override void Assign(Player player)
        {
            base.Assign(player);

            foreach (var item in Pickup.List.Where(item => item.Type == ItemType.GunCOM15))
            {
                if (item.Position.y is < 20 and > -10)
                {
                    item.Spawn();

                    player.SendConsoleMessage($"Пистолет находится в {Parse(item.Room.Type)}", "red");

                    break;
                }
            }
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
            if (!Players.Contains(ev.Attacker) || ev.DamageHandler.Type is DamageType.Unknown or DamageType.Falldown or DamageType.Warhead or DamageType.Decontamination or DamageType.Recontainment or DamageType.Crushed or DamageType.FemurBreaker or DamageType.PocketDimension or DamageType.SeveredHands)
            {
                return;
            }

            ev.Amount *= 1.03f;
        }

        private string Parse(RoomType room) => room switch
        {
            RoomType.LczCafe => "комнате с компьютерами",
            RoomType.Lcz173 => "оружейке К.С. SCP-173.",
            RoomType.LczToilets => "туалетах",
            RoomType.LczGlassBox => "стеклянном контейнере GR18",
            _ => room.ToString()
        };
    }
}
