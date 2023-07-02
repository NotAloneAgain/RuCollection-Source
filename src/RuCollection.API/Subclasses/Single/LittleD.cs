using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RuCollection.API.Subclasses.Single
{
    public sealed class LittleD : SingleSubclass
    {
        private static LittleD _singleton;

        private LittleD() { }

        public static LittleD Singleton => _singleton ??= new();

        public override string Name { get; } = "Карлик";

        public override RoleTypeId Role { get; } = RoleTypeId.ClassD;

        public override string Message { get; } = "Вы - карлик!\nВаш рост ниже, чем у остальных, из-за чего вас считают карликом.";

        public override string Color { get; } = "#009A63";

        public override float Health { get; } = 90;

        public override bool Show { get; } = true;

        public override Vector3 Size { get; } = Vector3.one * 0.64f;
    }
}
