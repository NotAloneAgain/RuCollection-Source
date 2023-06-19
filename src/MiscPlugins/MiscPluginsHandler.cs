using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Interfaces;
using Exiled.Events.EventArgs.Server;
using LightContainmentZoneDecontamination;
using MEC;
using MiscPlugins.Commands;
using MiscPlugins.CustomItems;
using PlayerRoles;
using Respawning;
using System.Collections.Generic;
using System.Linq;
using MiscPlugins.Lists;
using Utils.NonAllocLINQ;

namespace MiscPlugins.Handlers
{
    internal sealed class MiscPluginsHandler
    {
        private MiscPlugins.CassieDestroyed.Handlers.EventHandlers CassieDestroyedHandler;
        private MiscPlugins.RemoteKeycard.Handlers.EventHandlers RemoteKeycardHandler;
        private MiscPlugins.SCP500R.Handlers.EventHandlers SCP500RHandler;
        private MiscPlugins.UnlimitedAmmo.Handlers.EventHandlers UnlimitedAmmoHandler;

        public MiscPluginsHandler()
        {
            CassieDestroyedHandler = new CassieDestroyed.Handlers.EventHandlers();
            RemoteKeycardHandler = new RemoteKeycard.Handlers.EventHandlers();
            SCP500RHandler = new SCP500R.Handlers.EventHandlers();
            UnlimitedAmmoHandler = new UnlimitedAmmo.Handlers.EventHandlers();
        }
        public void Dispose()
        {
            CassieDestroyedHandler.UnsubscribeEvents();
            CassieDestroyedHandler = null;

            RemoteKeycardHandler.UnsubscribeEvents();
            RemoteKeycardHandler = null;

            SCP500RHandler.UnsubscribeEvents();
            SCP500RHandler = null;

            UnlimitedAmmoHandler.UnsubscribeEvents();
            UnlimitedAmmoHandler = null;
        }
private bool isWarheadCassie1Minute = false;
private bool isWarheadStart = false;
private bool isLightDecontStart = false;

        private void OnWaitingForPlayers()
        {
            Log.Info($"\nEnabling ControlNR.\nVersion: {Plugin.Singleton.Version}\nAuthor: {Plugin.Singleton.Author}");

            //Timing.KillCoroutines(WarheadMusic.ChangeColorsCoroutineHandle);
            //Timing.KillCoroutines(WarheadDecontamition.DecontamitionSequnse);

            if (true)//(Plugin.Singleton.Config.FullRoundRestart)
            {
                Log.Info("Setting NextRoundAction to full restart.");
                ServerStatic.StopNextRound = ServerStatic.NextRoundAction.Restart;
            }

            if (true)//(Plugin.Singleton.Config.RoomLootSpawn)
            {
                Log.Info("Spawning loot in rooms..");

                foreach (Door door in Door.List.Where(x => x.Type == Exiled.API.Enums.DoorType.LczArmory || x.Type == Exiled.API.Enums.DoorType.HczArmory))
                {
                    door.IsOpen = true;

                    door.IsOpen = false;
                }
            }

            Round.IsLobbyLocked = false;
            Res.DiedWithSCP500R.Clear();
            Res.RoleDiedWithSCP500R.Clear();
            Res.StatusEffectBase.Clear();
            CassieDestroyed.Handlers.EventHandlers.CassieDestroyedLVL = 0;
            GrenadeLauncher.CooldownIsEnable = false;
            isWarheadStart = false;
            isWarheadCassie1Minute = false;

            //ControlNR.Singleton.db.DropCollection("VIPPlayers");

            Server.FriendlyFire = false;
        }
        private void OnEndingRound(EndingRoundEventArgs ev)
        {
            if (!isWarheadCassie1Minute && Round.ElapsedTime.Minutes >= 34 && !Warhead.IsDetonated)
            {
                isWarheadCassie1Minute = true;
                Cassie.Message("Неизбежная детонация альфа-боеголовки будет запущена через 1<b></b> минуту.. <color=#ffffff00>h Alpha warhead detonation SEQUENCE .G1 will be started . in TMINUS . 1 minute", true, false, true);
            }
            if (!isWarheadStart && Round.ElapsedTime.Minutes >= 35 && !Warhead.IsDetonated)
            {
                isWarheadStart = true;

                if (Warhead.IsInProgress == false)
                {
                    Warhead.Controller.InstantPrepare();
                    Warhead.DetonationTimer = 120;
                    Warhead.Start();
                }
                Warhead.IsLocked = true;
            }



            if (!isLightDecontStart && Round.ElapsedTime.Minutes >= 5)
            {
                isLightDecontStart = true;

                DecontaminationController.Singleton.NetworkDecontaminationOverride = DecontaminationController.DecontaminationStatus.None;
            }
        }
        private void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Server.FriendlyFire = true;

            Cassie.Clear();
            Cassie.Message("Огонь по своим включён.. <color=#ffffff00>h pitch_1 F F enabled .g1", true, false, true);

            foreach (Pickup pickup in Pickup.List)
            {
                pickup.Destroy();
            }
            foreach (Ragdoll ragdoll in Ragdoll.List)
            {
                ragdoll.Destroy();
            }

            foreach (Player pl in Player.List)
            {
                pl.IsGodModeEnabled = false;
            }

            Timing.CallDelayed(0.1f, () =>
            {
                Respawn.ForceWave(Lists.Lists.RandomSpawnableTeamType.RandomItem(), false);
            });

            /*try
            {
                Control.API.Extensions.StopAudio();
            }
            catch (System.Exception) { }*/
        }
    }
}
