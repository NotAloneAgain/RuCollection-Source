using PlayerRoles;

namespace RuCollection.API.Subclasses.Single
{
    public sealed class GuardCaptain : SingleSubclass
    {
        private static GuardCaptain _singleton;

        private GuardCaptain() : base() { }

        public static GuardCaptain Singleton => _singleton ??= new();

        public override string Name { get; } = "Глава охраны";

        public override RoleTypeId Role { get; } = RoleTypeId.FacilityGuard;

        public override string Message { get; } = "Вы - глава охраны!\nВы можете руководить другими охранниками так как более профессиональны.";

        public override bool Show { get; } = true;

        public override Inventory Inventory { get; } = new Inventory(new(6)
        {
            { 0, new (1)
            {
                { ItemType.KeycardNTFOfficer, 100 },
            }
            },
            { 1, new (1)
            {
                { ItemType.GunCrossvec, 100 },
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
            { 5, new(1)
            {
                {ItemType.ArmorHeavy, 100 },
            }
            },
        });
    }
}
