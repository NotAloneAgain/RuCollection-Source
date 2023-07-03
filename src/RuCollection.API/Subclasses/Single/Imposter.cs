using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;
using RuCollection.API.Subclasses.Group;
using UnityEngine;

namespace RuCollection.API.Subclasses.Single
{
    public sealed class Imposter : GroupSubclass
    {
        private static Imposter _singleton;

        private Imposter() { }

        public static Imposter Singleton => _singleton ??= new();

        public override string Name { get; } = "Предатель";

        public override RoleTypeId Role { get; } = RoleTypeId.FacilityGuard;

        public override string Message { get; } = "Вы - предатель!\nИспользуйте команду \".rifleman\", в консоли (\"ё\") чтобы переодеться в повтанца хаоса.";

        public override bool Show { get; } = true;

        public override Inventory Inventory { get; } = new Inventory(new(7)
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
                {ItemType.ArmorLight, 100 },
            }
            },
        });

        public override void Assign(Player player)
        {
            base.Assign(player);

            player.GetEffect(EffectType.Asphyxiated).ServerSetState(1, 3600, false);
        }
    }
}
