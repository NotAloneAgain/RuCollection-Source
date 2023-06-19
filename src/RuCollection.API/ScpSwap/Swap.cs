using PlayerRoles;
using System.Collections.Generic;
using Exiled.API.Features;

namespace RuCollection.API.ScpSwap
{
    internal static class Swap
    {
        public static ushort SwapDuration { get; set; }

        public static List<RoleTypeId> AllowedScps { get; set; }

        public static Dictionary<RoleTypeId, int> Slots { get; set; }
    }
}
