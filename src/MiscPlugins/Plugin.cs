using Exiled.CustomItems.API.Features;
using Exiled.CustomRoles.API.Features;
using HarmonyLib;
using MiscPlugins.Configs;
using MiscPlugins.Handlers;
using PluginAPI.Core.Attributes;
using PluginAPI.Events;
using System;

namespace MiscPlugins
{
    public sealed class Plugin : Exiled.API.Features.Plugin<Config>
    {
        private const string HarmonyId = "swd.MiscPlugins.Patches";

        private MiscPluginsHandler _handlers;
        private Harmony _harmony;
        public static Plugin Singleton;
        public override string Name => "MiscPlugins";
        public override string Author => "swd && .grey#9120";

        public override Version Version => new(1, 0, 0);

        public override void OnEnabled()
        {
            Singleton = this;

            CustomItem.RegisterItems();
            CustomRole.RegisterRoles(false, null, true, Assembly);

            _harmony = new(HarmonyId);
            _handlers = new MiscPluginsHandler();

            _harmony.PatchAll(GetType().Assembly);

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            _harmony.UnpatchAll(HarmonyId);

            _handlers = null;
            _harmony = null;

            Singleton = null;

            base.OnDisabled();
        }
    }
}
