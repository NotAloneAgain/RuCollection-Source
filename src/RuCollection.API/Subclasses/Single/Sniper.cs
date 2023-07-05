using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;
using RuCollection.API.Subclasses.Group;
using UnityEngine;

namespace RuCollection.API.Subclasses.Single
{
    public sealed class Sniper : GroupSubclass
    {
        private static Sniper _singleton;

        private Sniper() { }

        public static Sniper Singleton => _singleton ??= new();

        public override string Name { get; } = "Снайпер";

        public override RoleTypeId Role { get; } = RoleTypeId.FacilityGuard;

        public override string Message { get; } = "Вы - снайпер!\nУ вас есть винтовка.";

        public override Inventory Inventory { get; } = new Inventory(new(7)
        {
            { 0, new (1)
            {
                { ItemType.KeycardGuard, 100 },
            }
            },
            { 1, new (1)
            {
                { ItemType.GunE11SR, 100 },
            }
            },
            { 2, new (1)
            {
                { ItemType.Radio, 100 },
            }
            },
            { 3, new (1)
            {
                { ItemType.GrenadeFlash, 100 },
            }
            },
            { 4, new (1)
            {
                {ItemType.Medkit, 100 },
            }
            },
            { 5, new (1)
            {
                {ItemType.Adrenaline, 100 },
            }
            },
            { 6, new(1)
            {
                {ItemType.ArmorLight, 100 },
            }
            },
        });
    }
}
