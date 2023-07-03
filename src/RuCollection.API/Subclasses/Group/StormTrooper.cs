using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;
using UnityEngine;

namespace RuCollection.API.Subclasses.Group
{
    public sealed class StormTrooper : GroupSubclass
    {
        private static StormTrooper _singleton;

        private StormTrooper() : base(2) { }

        public static StormTrooper Singleton => _singleton ??= new();

        public override string Name { get; } = "Штурмовик";

        public override RoleTypeId Role { get; } = RoleTypeId.FacilityGuard;

        public override string Message { get; } = "Вы - штурмовик!\nЯвляетесь сильной боевой единицей с хорошей защитой.";

        public override bool Show { get; } = true;

        public override Inventory Inventory { get; } = new Inventory(new(6)
        {
            { 0, new (1)
            {
                { ItemType.KeycardGuard, 100 },
            }
            },
            { 1, new (1)
            {
                { ItemType.GunFSP9, 100 },
            }
            },
            { 2, new (1)
            {
                {ItemType.Medkit, 100 },
            }
            },
            { 3, new (1)
            {
                { ItemType.GrenadeFlash, 100 },
            }
            },
            { 4, new (1)
            {
                { ItemType.Radio, 100 },
            }
            },
            { 5, new(1)
            {
                {ItemType.ArmorHeavy, 100 }
            }
            }
        } );

        public override void Assign(Player player)
        {
            base.Assign(player);
        }
    }
}
