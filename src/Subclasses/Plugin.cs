﻿using Exiled.Events.Handlers;
using RuCollection.API.Global;
using Subclasses.Configs;
using Subclasses.Handlers;
using System;

namespace Subclasses
{
    public sealed class Plugin : PluginWithData<Config>
    {
        private PlayerHandlers _handlers;

        public override string Name => "Subclasses";

        public override string Prefix => "Subclasses";

        public override string Author => ".grey#9120";

        public override Version Version => new(1, 0, 0);

        public override void OnEnabled()
        {
            _handlers = new();

            Player.ChangingRole += _handlers.OnChangingRole;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Player.ChangingRole -= _handlers.OnChangingRole;

            _handlers = null;

            base.OnDisabled();
        }

        public override void OnReloaded() { }

        public override void OnRegisteringCommands() { }

        public override void OnUnregisteringCommands() { }

        public override void Reset()
        {
            _handlers.Reset();
        }
    }
}
