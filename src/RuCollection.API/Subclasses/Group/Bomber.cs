using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;
using UnityEngine;

namespace RuCollection.API.Subclasses.Group
{
    public sealed class Bomber : GroupSubclass
    {
        private static Bomber _singleton;

        private Bomber() : base(2) { }

        public static Bomber Singleton => _singleton ??= new();

        public override string Name { get; } = "Подрывник";

        public override RoleTypeId Role { get; } = RoleTypeId.FacilityGuard;

        public override string Message { get; } = "Вы - подрывник!\nУ вас есть осколочная граната и дробовик.";

        public override Inventory Inventory { get; } = new Inventory(new(7)
        {
            { 0, new (1)
            {
                { ItemType.KeycardGuard, 100 },
            }
            },
            { 1, new (1)
            {
                { ItemType.GunShotgun, 100 },
            }
            },
            { 2, new (1)
            {
                { ItemType.Radio, 100 },
            }
            },
            { 3, new (1)
            {
                { ItemType.GrenadeHE, 100 },
            }
            },
            { 4, new (1)
            {
                {ItemType.Medkit, 100 },
            }
            },
            { 5, new(1)
            {
                {ItemType.ArmorCombat, 100 },
            }
            },
        });

        public override void Assign(Player player)
        {
            base.Assign(player);
        }
    }
}
