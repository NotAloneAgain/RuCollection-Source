using Exiled.API.Features;
using Exiled.API.Interfaces;

namespace RuCollection.API.Global
{
    public abstract class PluginWithData<TConfig> : Plugin<TConfig> where TConfig : IConfig, new()
    {
        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Server.RestartingRound += Reset;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.RestartingRound -= Reset;

            base.OnDisabled();
        }

        public abstract void Reset();
    }
}
