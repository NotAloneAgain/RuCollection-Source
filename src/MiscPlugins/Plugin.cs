﻿using Exiled.Events.Handlers;
using HarmonyLib;
using MiscPlugins.Configs;
using MiscPlugins.Handlers;
using RuCollection.API.Global;
using System;

namespace MiscPlugins
{
    public sealed class Plugin : PluginWithData<Config>
    {
        private const string HarmonyId = "Ray-Grey.RoundScenarious";

        private Harmony _harmony;
        private PlayerHandlers _playerHandlers;
        private ServerHandlers _serverHandlers;
        private WarheadHandlers _warheadHandlers;

        public override string Name => "MiscPlugins";

        public override string Prefix => "MiscPlugins";

        public override string Author => ".grey#9120";

        public override Version Version => new(1, 0, 0);

        public override void OnEnabled()
        {
            _harmony = new(HarmonyId);
            _serverHandlers = new();
            _playerHandlers = new(Config.WeaponHintText);
            _warheadHandlers = new();

            _harmony.PatchAll(GetType().Assembly);

            Player.Shot += _playerHandlers.OnShot;
            Player.Dying += _playerHandlers.OnDying;
            Player.Hurting += _playerHandlers.OnHurting;
            Player.UsedItem += _playerHandlers.OnUsedItem;
            Player.ChangingRole += _playerHandlers.OnChangingRole;
            Player.TriggeringTesla += _playerHandlers.OnTriggeringTesla;
            Player.ReloadingWeapon += _playerHandlers.OnReloadingWeapon;
            Player.InteractingDoor += _playerHandlers.OnInteractingDoor;
            Player.InteractingLocker += _playerHandlers.OnInteractingLocker;
            Player.UnlockingGenerator += _playerHandlers.OnUnlockingGenerator;
            Player.ActivatingGenerator += _playerHandlers.OnActivatingGenerator;

            Server.RoundStarted += _serverHandlers.OnRoundStarted;

            Warhead.Detonated += _warheadHandlers.OnDetonated;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Warhead.Detonated -= _warheadHandlers.OnDetonated;

            Server.RoundStarted -= _serverHandlers.OnRoundStarted;

            Player.ActivatingGenerator -= _playerHandlers.OnActivatingGenerator;
            Player.UnlockingGenerator -= _playerHandlers.OnUnlockingGenerator;
            Player.InteractingLocker -= _playerHandlers.OnInteractingLocker;
            Player.InteractingDoor -= _playerHandlers.OnInteractingDoor;
            Player.ReloadingWeapon -= _playerHandlers.OnReloadingWeapon;
            Player.TriggeringTesla -= _playerHandlers.OnTriggeringTesla;
            Player.ChangingRole -= _playerHandlers.OnChangingRole;
            Player.UsedItem -= _playerHandlers.OnUsedItem;
            Player.Hurting -= _playerHandlers.OnHurting;
            Player.Dying -= _playerHandlers.OnDying;
            Player.Shot -= _playerHandlers.OnShot;

            _harmony.UnpatchAll(HarmonyId);

            _playerHandlers = null;
            _harmony = null;

            base.OnDisabled();
        }

        public override void OnReloaded() { }

        public override void OnRegisteringCommands() { }

        public override void OnUnregisteringCommands() { }

        public override void Reset()
        {
            _serverHandlers?.Reset();
        }
    }
}
