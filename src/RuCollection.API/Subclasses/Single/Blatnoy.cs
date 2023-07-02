using PlayerRoles;

namespace RuCollection.API.Subclasses.Single
{
    public sealed class Blatnoy : SingleSubclass
    {
        private static Blatnoy _singleton;

        private Blatnoy() { }

        public static Blatnoy Singleton => _singleton ??= new();

        public override string Name { get; } = "Блатной";

        public override RoleTypeId Role { get; } = RoleTypeId.ClassD;

        public override string Message { get; } = "Вы - блатной!\nК вам относиться с уважением каждый дешка, а постоянное принятие адреналина и наркотиков закалило вас.";

        public override (float Amount, float Limit, float Decay, float Efficacy, float sus, bool Persistent) Shield { get; } = (15, 15, -0.5f, 1, 5, true);

        public override Inventory Inventory { get; } = new Inventory(new(1)
        {
            { 0, new (1)
            {
                { ItemType.Adrenaline, 100 },
            }
            }
        });
    }
}
