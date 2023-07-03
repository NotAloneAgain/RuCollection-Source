using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;
using UnityEngine;

namespace RuCollection.API.Subclasses.Group
{
    public sealed class GuardCaptain : GroupSubclass
    {
        private static GuardCaptain _singleton;

        private GuardCaptain() : base(3) { }

        public static GuardCaptain Singleton => _singleton ??= new();

        public override string Name { get; } = "Глава охраны";

        public override RoleTypeId Role { get; } = RoleTypeId.FacilityGuard;

        public override string Message { get; } = "Вы - глава охраны!\nВаша экипировка лучше чем у остальных.";

        public override bool Show { get; } = true;

        public override Inventory Inventory { get; } = new Inventory(new(6)
        {
            { 0, new (1)
            {
                { ItemType.KeycardNTFLieutenant, 100 },
            }
            },
            { 1, new (1)
            {
                { ItemType.GunCrossvec, 100 },
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
        });

        public override void Assign(Player player)
        {
            base.Assign(player);
        }
    }
}
