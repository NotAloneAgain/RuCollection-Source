using Exiled.API.Features;
using PlayerRoles;

namespace RuCollection.API.Subclasses.Group
{
    public sealed class JuniorGuard : GroupSubclass
    {
        private static JuniorGuard _singleton;

        private JuniorGuard() : base(3) { }

        public static JuniorGuard Singleton => _singleton ??= new();

        public override string Name { get; } = "Младший сотрудник";

        public override RoleTypeId Role { get; } = RoleTypeId.FacilityGuard;

        public override string Message { get; } = "Вы - младший сотрудник!\nВы совсем не давно устроились в фонд.";

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
                { ItemType.Radio, 100 },
            }
            },
            { 4, new(1)
            {
                {ItemType.ArmorLight, 100 }
            }
            }
        });
    }
}
