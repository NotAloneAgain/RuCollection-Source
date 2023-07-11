using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using PlayerRoles;
using UnityEngine;

namespace RuCollection.API.Subclasses.Single
{
    public sealed class Engineer : SingleSubclass
    {
        private static Engineer _singleton;

        private Engineer() { }

        public static Engineer Singleton => _singleton ??= new();

        public override string Name { get; } = "Инженер";

        public override RoleTypeId Role { get; } = RoleTypeId.Scientist;

        public override string Message { get; } = "Вы - инженер!\nВы работали над камерами содержания объектов, поэтому заметили неисправность заранее и успели взять пистолет в комнате охраны.";

        public override bool Show { get; } = true;

        public override Inventory Inventory { get; } = new Inventory(new(2)
        {
            { 0, new (1)
            {
                { ItemType.KeycardContainmentEngineer, 100 },
            }
            },
            { 1, new (1)
            {
                { ItemType.GunCOM15, 100 },
            }
            }
        });

        public override void Assign(Player player)
        {
            base.Assign(player);

            Timing.CallDelayed(0.0005f, () => player.Teleport(RoomType.HczHid));
        }
    }
}
