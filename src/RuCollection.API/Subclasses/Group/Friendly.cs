using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using PlayerRoles;
using RuCollection.API.Subclasses.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RuCollection.API.Subclasses.Group
{
    public sealed class Friendly : GroupSubclass
    {
        private static Friendly _singleton;

        private Friendly() : base(3) { }

        public static Friendly Singleton
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

        public override string Name { get; } = "Дружелюбный";

        public override RoleTypeId Role { get; } = RoleTypeId.Scientist;

        public override string Message { get; } = "Вы - дружелюбный!\nВы имеете 120 единиц здоровья и появляется в PC.";

        public override float Health { get; } = 120;

        public override Inventory Inventory { get; } = new Inventory(new(2)
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
        });

        public override void Assign(Player player)
        {
            base.Assign(player);

            Vector3 pos = Room.List.First(x => x.Type == RoomType.LczCafe).Position + Vector3.up;

            Timing.CallDelayed(0.0005f, () => player.Teleport(pos));
        }
    }
}
