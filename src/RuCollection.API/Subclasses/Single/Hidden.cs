using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using RuCollection.API.Subclasses.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuCollection.API.Subclasses.Single
{
    public sealed class Hidden : SingleSubclass
    {
        private static Hidden _singleton;

        private Hidden() { }

        public static Hidden Singleton => _singleton ??= new();

        public override string Name { get; } = "Скрытный";

        public override RoleTypeId Role { get; } = RoleTypeId.Scientist;

        public override string Message { get; } = "Вы - скрытный!\nПо команде .hide вы можете стать невидимым на 20 секунд. Перезарядка: 3 минуты.";

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
    }
}
