using Exiled.Events.Handlers;
using RemoteAdmin;
using RuCollection.API.ScpSwap;
using ScpSwap.Commands;
using ScpSwap.Configs;
using ScpSwap.Handlers;
using System;

namespace ScpSwap
{
    public sealed class Plugin : Exiled.API.Features.Plugin<Config>
    {
        private PlayerHandlers _playerHandlers;
        private Force _command;

        public override string Name => "ScpSwap";

        public override string Prefix => "ScpSwap";

        public override string Author => ".grey#9120";

        public override Version Version => new (1, 0, 0);

        public override void OnEnabled()
        {
            _playerHandlers = new(Config.InfoText, Config.InfoDuration);

            Swap.Slots = Config.Slots;
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
            Swap.Slots = null;

            _playerHandlers = null;

            base.OnDisabled();
        }

        public override void OnReloaded() { }

        public override void OnRegisteringCommands()
        {
            _command = new (Config.PreventMultipleSwaps);

            QueryProcessor.DotCommandHandler.RegisterCommand(_command);
        }

        public override void OnUnregisteringCommands()
        {
            QueryProcessor.DotCommandHandler.UnregisterCommand(_command);

            _command = null;
        }
    }
}
