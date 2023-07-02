using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using PlayerRoles;
using System.Linq;
using UnityEngine;

namespace RuCollection.API.Subclasses.Single
{
    public sealed class Manager : SingleSubclass
    {
        private static Manager _singleton;

        private Manager() { }

        public static Manager Singleton => _singleton ??= new();

        public override string Name { get; } = "Менеджер зоны";

        public override RoleTypeId Role { get; } = RoleTypeId.Scientist;

        public override string Message { get; } = "Вы - менеджер зоны!\nВы появляетесь с картой менеджера зоны, аптечкой и бронежилетом под одеждой. На вас не реагируют теслы.";

        public override bool Show { get; } = true;

        public override Inventory Inventory { get; } = new Inventory(new(3)
        {
            { 0, new (1)
            {
                { ItemType.KeycardFacilityManager, 100 },
            }
            },
            { 1, new (1)
            {
                { ItemType.Medkit, 100 },
            }
            },
            { 2, new (1)
            {
                { ItemType.ArmorLight, 100 },
            }
            },
        });

        public override void Assign(Player player)
        {
            base.Assign(player);

            Vector3 pos = Room.List.First(room => room.Type == RoomType.HczTestRoom).LocalPosition(Vector3.forward * 6.5f) + Vector3.up;

            Timing.CallDelayed(0.0005f, () => player.Teleport(pos));
        }
    }
}
