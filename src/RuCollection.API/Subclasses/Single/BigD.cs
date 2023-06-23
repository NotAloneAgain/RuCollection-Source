using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RuCollection.API.Subclasses.Single
{
    public sealed class BigD : SingleSubclass
    {
        private static BigD _singleton;

        private BigD() { }

        public static BigD Singleton
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

        public override string Name { get; } = "Гигант";

        public override RoleTypeId Role { get; } = RoleTypeId.ClassD;

        public override string Message { get; } = "Вы - гигант!\nВаш рост выше, чем у остальных, из-за чего вас считают гигантом.";

        public override string Color { get; } = "#009A63";

        public override float Health { get; } = 150;

        public override bool Show { get; } = true;

        public override Vector3 Size { get; } = Vector3.one * 1.11f;
    }
}
