using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using System.Linq;
using UnityEngine;

namespace MiscPlugins.Handlers
{
    internal sealed class WarheadHandlers
    {
        static WarheadHandlers();

        public void OnDetonated()
        {
            foreach (var item in Pickup.List)
            {
                if (item.Room == null || item.Room.Zone == ZoneType.Surface)
                {
                    continue;
                }

                item.Destroy();
            }

            foreach (var ragdoll in Ragdoll.List)
            {
                if (ragdoll.Room == null || ragdoll.Room.Zone == ZoneType.Surface)
                {
                    continue;
                }

                ragdoll.Destroy();
            }

            OptimizeEverything();
        }

        private static void OptimizeEverything();

        // Code
    }
}
