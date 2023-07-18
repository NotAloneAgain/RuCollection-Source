using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;

namespace RuCollection.API.Subclasses.Group
{
    public sealed class Thief : GroupSubclass
    {
        private static Thief _singleton;

        private Thief() : base(2) { }

        public static Thief Singleton => _singleton ??= new();

        public override string Name { get; } = "Профессиональный вор";

        public override RoleTypeId Role { get; } = RoleTypeId.ClassD;

        public override string Message { get; } = "Вы - Профессиональный вор! Когда-то давно вы ограбили банк и попали сюда.\nВы имеете хороший стартовый набор, команду .steal и +2% к скорости.";

        public override bool KeepOnEscape { get; } = true;

        public override Inventory Inventory { get; } = new Inventory(new(1)
        {
            { 0, new (1)
            {
                { ItemType.KeycardJanitor, 100 },
            }
            },
            { 1, new (1)
            {
                { ItemType.Medkit, 100 },
            }
            },
            { 2, new (1)
            {
                { ItemType.Radio, 100 },
            }
            }
        });

        public override void Assign(Player player)
        {
            base.Assign(player);

            player.GetEffect(EffectType.MovementBoost)?.ServerSetState(2, 3600, false);
        }
    }
}
