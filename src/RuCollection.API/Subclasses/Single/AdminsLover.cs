using PlayerRoles;
using UnityEngine;

namespace RuCollection.API.Subclasses.Single
{
    internal class AdminsLover : SingleSubclass
    {
        private static AdminsLover _singleton;

        private AdminsLover() { }

        public static AdminsLover Singleton => _singleton ??= new();

        public override string Name { get; } = "Любимка администрации";

        public override RoleTypeId Role { get; } = RoleTypeId.ClassD;

        public override string Message { get; } = "Вы - любимка администрации!\nУ вас маленький рост.";

        public override string Color { get; } = "#009A63";

        public override bool Show { get; } = true;

        public override Vector3 Size { get; } = Vector3.one * 0.5f;
    }
}
