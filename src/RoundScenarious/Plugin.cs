using HarmonyLib;
using PluginAPI.Core.Attributes;
using PluginAPI.Events;
using RoundScenarious.Configs;
using RoundScenarious.Handlers;
using System;

namespace RoundScenarious
{
    public sealed class Plugin : Exiled.API.Features.Plugin<Config>
    {
        private const string HarmonyId = "Ray-Grey.RoundScenarious";

        private EventHandlers _handlers;
        private Harmony _harmony;

        public override string Name => "RoundScenarious";

        public override string Prefix => "RoundScenarious";

        public override string Author => ".grey#9120";

        public override Version Version => new(1, 0, 0);

        public override void OnEnabled()
        {
            _harmony = new(HarmonyId);
            _handlers = new();

            _harmony.PatchAll(GetType().Assembly);

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            _harmony.UnpatchAll(HarmonyId);

            _handlers = null;
            _harmony = null;

            base.OnDisabled();
        }

        public override void OnReloaded() { }

        public override void OnRegisteringCommands() { }

        public override void OnUnregisteringCommands() { }
    }
}
