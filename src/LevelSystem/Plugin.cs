using HarmonyLib;
using LevelSystem.Handlers;
using LiteDB;
using System;
using System.IO;

namespace LevelSystem
{
    public sealed class Plugin : Exiled.API.Features.Plugin<Config>
    {
        private const string HarmonyId = "swd.LevelSystem.Patches";

        private EventHandlers _handlers;
        private Harmony _harmony;

        public override string Name => "LevelSystem";
        public override string Author => "swd";
        public static Plugin Singleton;
        public override Version Version => new(1, 0, 0);
        public LiteDatabase db;
        public override void OnEnabled()
        {
            _harmony = new(HarmonyId);
            _handlers = new EventHandlers();

            db = new LiteDatabase(Path.Combine(Exiled.API.Features.Paths.Configs, @"Plugin/LimitDonator.db"));

            _harmony.PatchAll(GetType().Assembly);

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            _harmony.UnpatchAll(HarmonyId);

            db.Dispose();
            db = null;

            _handlers = null;
            _harmony = null;

            base.OnDisabled();
        }
    }
}
