using PlayerRoles;

namespace RuCollection.API.Subclasses.Single
{
    public sealed class Lead : SingleSubclass
    {
        private static Lead _singleton;

        private Lead() { }

        public static Lead Singleton
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

        public override string Name { get; } = "Руководитель";

        public override RoleTypeId Role { get; } = RoleTypeId.Scientist;

        public override string Message { get; } = "Вы - руководитель ученого персонала!\nВы появляетесь с картой научного руководителя, аптечкой и бронежилетом под одеждой.";

        public override bool Show { get; } = true;

        public override Inventory Inventory { get; } = new Inventory(new(8)
        {
            { 0, new (1)
            {
                { ItemType.KeycardResearchCoordinator, 100 },
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
    }
}
