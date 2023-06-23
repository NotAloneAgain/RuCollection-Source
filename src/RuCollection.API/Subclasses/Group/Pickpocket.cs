﻿using PlayerRoles;

namespace RuCollection.API.Subclasses.Group
{
    public sealed class Pickpocket : GroupSubclass
    {
        private static Pickpocket _singleton;

        private Pickpocket() : base() { }

        public static Pickpocket Singleton
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

        public override string Name { get; } = "Карманник";

        public override RoleTypeId Role { get; } = RoleTypeId.ClassD;

        public override string Message { get; } = "Вы - карманник!\nВы появляетесь с тем, что смогли стащить у сотрудников комплекса и можете воровать командой .steal.";

        public override Inventory Inventory { get; } = new Inventory(new (1)
        {
            { 0, new (7)
            {
                { ItemType.None,           20 },
                { ItemType.KeycardJanitor, 10 },
                { ItemType.Coin,           40 },
                { ItemType.Radio,          30 },
                { ItemType.GrenadeFlash,   20 },
                { ItemType.ArmorLight,     15 },
                { ItemType.Flashlight,     100 },
            }
            }
        });
    }
}
