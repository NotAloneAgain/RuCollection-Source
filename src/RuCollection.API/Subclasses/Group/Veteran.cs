using PlayerRoles;

namespace RuCollection.API.Subclasses.Group
{
    public sealed class Veteran : GroupSubclass
    {
        private static Veteran _singleton;

        private Veteran() : base() { }

        public static Veteran Singleton => _singleton ??= new();

        public override string Name { get; } = "Бывалый";

        public override RoleTypeId Role { get; } = RoleTypeId.ClassD;

        public override string Message { get; } = "Вы - бывалый!\nУ вас повышенное количество здоровья и таблетки обезболивающего.";

        public override float Health { get; } = 110;

        public override Inventory Inventory { get; } = new Inventory(new(1)
        {
            { 0, new (1)
            {
                { ItemType.Painkillers, 100 },
            }
            }
        });
    }
}
