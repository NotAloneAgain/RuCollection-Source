using PlayerRoles;

namespace RuCollection.API.Subclasses.Group
{
    public sealed class Medic : GroupSubclass
    {
        private static Medic _singleton;

        private Medic() : base(2) { }

        public static Medic Singleton => _singleton ??= new();

        public override string Name { get; } = "Медик";

        public override RoleTypeId Role { get; } = RoleTypeId.Scientist;

        public override string Message { get; } = "Вы - медик!\nВы появляетесь с большим количеством лекарственных препаратов.";

        public override bool Show { get; } = true;

        public override Inventory Inventory { get; } = new Inventory(new(6)
        {
            { 0, new (1)
            {
                { ItemType.KeycardScientist, 100 },
            }
            },
            { 1, new (1)
            {
                { ItemType.Medkit, 100 },
            }
            },
            { 2, new (1)
            {
                { ItemType.Medkit, 100 },
            }
            },
            { 3, new (1)
            {
                { ItemType.Painkillers, 100 },
            }
            },
            { 4, new (1)
            {
                { ItemType.Painkillers, 100 },
            }
            },
            { 5, new (1)
            {
                { ItemType.Adrenaline, 50 },
            }
            },
        });

    }
}
