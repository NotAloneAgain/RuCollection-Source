using Exiled.Events.Handlers;
using RuCollection.API.ScpSwap;
using ScpSwap.Configs;
using ScpSwap.Handlers;
using System;

namespace ScpSwap
{
    public sealed class Plugin : Exiled.API.Features.Plugin<Config>
    {
        private PlayerHandlers _playerHandlers;

        public override string Name { get; } = "ScpSwap";

        public override string Prefix { get; } = "ScpSwap";

        public override string Author { get; } = ".grey#9120";

        public override Version Version { get; } = new (1, 0, 0);

        public override void OnEnabled()
        {
            _playerHandlers = new(Config.InfoText, Config.InfoDuration);

            Swap.Slots = Config.Slots;
            Swap.Prevent = Config.PreventMultipleSwaps;
            Swap.AllowedScps = Config.AllowedScps;
            Swap.SwapDuration = Config.SwapDuration;

            Player.ChangingRole += _playerHandlers.OnChangingRole;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Player.ChangingRole -= _playerHandlers.OnChangingRole;

            Swap.SwapDuration = 0;
            Swap.AllowedScps = null;
            Swap.Prevent = false;
            Swap.Slots = null;

            _playerHandlers = null;

            base.OnDisabled();
        }

        public override void OnReloaded() { }

        public override void OnRegisteringCommands() { }

        public override void OnUnregisteringCommands() { }
    }
}
