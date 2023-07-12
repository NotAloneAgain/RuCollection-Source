using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using MEC;
using RuCollection.API.Global;
using System.Collections.Generic;

namespace MiscPlugins.Handlers
{
    internal sealed class ServerHandlers : IHasData
    {
        private CoroutineHandle[] _coroutines;

        internal ServerHandlers()
        {
            _coroutines = new CoroutineHandle[2];
        }

        public void OnRoundStarted()
        {
            _coroutines[0] = Timing.RunCoroutine(_CleanupItems());
            _coroutines[1] = Timing.RunCoroutine(_CleanupRagdolls());
        }

        public IEnumerator<float> _CleanupItems()
        {
            List<Pickup> toClear = new(500);

            while (true)
            {
                foreach (var item in Pickup.List)
                {
                    if (!item.IsSpawned)
                    {
                        continue;
                    }

                    if (!toClear.Contains(item))
                    {
                        toClear.Add(item);

                        continue;
                    }

                    item.Destroy();
                }

                Timing.WaitForSeconds(240);
            }
        }

        public IEnumerator<float> _CleanupRagdolls()
        {
            List<Ragdoll> toClear = new(100);

            while (true)
            {
                foreach (var ragdoll in Ragdoll.List)
                {
                    if (!toClear.Contains(ragdoll) && (ragdoll.IsExpired || ragdoll.IsConsumed) && ragdoll.CanBeCleanedUp && ragdoll.AllowCleanUp)
                    {
                        toClear.Add(ragdoll);

                        continue;
                    }

                    ragdoll.Destroy();
                }

                Timing.WaitForSeconds(120);
            }
        }

        public void Reset()
        {
            Timing.KillCoroutines(_coroutines);
        }
    }
}
