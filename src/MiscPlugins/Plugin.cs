using MiscPlugins.Configs;
using MiscPlugins.Handlers;
using Exiled.Events.Handlers;
using System;
using HarmonyLib;

namespace MiscPlugins
{
    public sealed class Plugin : Exiled.API.Features.Plugin<Config>
    {
        private const string HarmonyId = "Ray-Grey.RoundScenarious";

        private Harmony _harmony;
        private PlayerHandlers _playerHandlers;

        public override string Name => "MiscPlugins";

        public override string Prefix => "MiscPlugins";

        public override string Author => ".grey#9120";

        public override Version Version => new(1, 0, 0);

        public override void OnEnabled()
        {
            _harmony = new(HarmonyId);
            _playerHandlers = new();

            _harmony.PatchAll(GetType().Assembly);

            Player.ChangingRole += _playerHandlers.OnChangingRole;
            Player.ReloadingWeapon += _playerHandlers.OnReloadingWeapon;
            Player.InteractingDoor += _playerHandlers.OnInteractingDoor;
            Player.InteractingLocker += _playerHandlers.OnInteractingLocker;
            Player.UnlockingGenerator += _playerHandlers.OnUnlockingGenerator;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Player.UnlockingGenerator -= _playerHandlers.OnUnlockingGenerator;
            Player.InteractingLocker -= _playerHandlers.OnInteractingLocker;
            Player.InteractingDoor -= _playerHandlers.OnInteractingDoor;
            Player.ReloadingWeapon -= _playerHandlers.OnReloadingWeapon;
            Player.ChangingRole -= _playerHandlers.OnChangingRole;

            _harmony.UnpatchAll(HarmonyId);

            _playerHandlers = null;
            _harmony = null;

            base.OnDisabled();
        }

        public override void OnReloaded() { }

        public override void OnRegisteringCommands() { }

        public override void OnUnregisteringCommands() { }
    }
}
