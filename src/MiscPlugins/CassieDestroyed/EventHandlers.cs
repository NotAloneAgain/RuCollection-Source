using Exiled.API.Features;
using Exiled.Events.EventArgs.Interfaces;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Server;
using HarmonyLib;
using MEC;
using MiscPlugins.Commands;
using MiscPlugins.CustomItems;
using Respawning;
using System.Linq;
using Utils.NonAllocLINQ;

namespace MiscPlugins.CassieDestroyed.Handlers
{
    internal sealed class EventHandlers
    {
        public static int CassieDestroyedLVL = 0;
        public EventHandlers()
        {
            Exiled.Events.Handlers.Server.RespawningTeam += OnRespawningTeam;
            Exiled.Events.Handlers.Map.AnnouncingScpTermination += OnAnnouncingScpTermination;
            Exiled.Events.Handlers.Map.AnnouncingNtfEntrance += OnAnnouncingNtfEntrance;
        }
        ~EventHandlers()
        {
            Exiled.Events.Handlers.Server.RespawningTeam -= OnRespawningTeam;
            Exiled.Events.Handlers.Map.AnnouncingScpTermination -= OnAnnouncingScpTermination;
            Exiled.Events.Handlers.Map.AnnouncingNtfEntrance -= OnAnnouncingNtfEntrance;
        }
        private void OnRespawningTeam(RespawningTeamEventArgs ev)
        {
            if (!ev.IsAllowed) return;

            if (ev.NextKnownTeam == SpawnableTeamType.ChaosInsurgency)
            {
                switch (CassieDestroyedLVL)
                {
                    case 0:
                        CassieDestroyedLVL += 1;
                        Cassie.Message("О̸̲̮͆паc█ос█ь! Внешний ██риме̸т̵р нару█ен неи███стным отря██м. Всему персоналу р███енд█ется укрыться в безо̸̙̌п̸̖͐̀асном м████.. <color=#ffffff00>h pitch_0.15 .g4 . .g4 . pitch_0.6 danger .g2 . pitch_0.7 external pitch_0.5 .g4 jam_1_1 board r was pitch_8 breached by  . pitch_0.6 .g4 . pitch_0.7 an unknown unit . all remaining personnel . pitch_0.6 .g6 . are advised to take shelter in a safe location </color>", false, false, true);
                        break;
                    case 1:
                        CassieDestroyedLVL += 1;
                        Cassie.Message("pitch_0.09 jam_006_1 .G4 .G6 .G3", false, false, false);
                        break;
                    case 2:
                        CassieDestroyedLVL += 1;
                        Cassie.Message("jam_040_9 pitch_0.43 .G1 . jam_020_9 .G3 . .G5 . pitch_0.3 .g3 . . . pitch_0.2 .g1", false, false, false);
                        break;
                    case 3:
                        Map.TurnOffAllLights(15f);
                        Cassie.Message("pitch_0.03 .g7", false, false, false);
                        break;
                }
            }
        }
        private void OnAnnouncingScpTermination(AnnouncingScpTerminationEventArgs ev)
        {
            switch (CassieDestroyedLVL)
            {
                case 3:
                    ev.IsAllowed = false;
                    Cassie.Message("SCP-███<b></b> успе█нjjjj у̸н̸и̴ч̸т████. При█ин█ с̸͓̍м̶̟͛е̵̰̰̽̈р̵͍͑ти - н̸е̸известна.. <color=#ffffff00>h pitch_0.8 SCP . pitch_0.6 .G6 .g1 . .g1 . .g1 . has . been .g3 . SUCCESSFULLY . .g4 terminated . .g4 . termination cause is pitch_0.5 unspecified . .g5 . pitch_0.3 .g5 pitch_0.1 .g5", false, false, true);
                    break;
            }
        }
        private void OnAnnouncingNtfEntrance(AnnouncingNtfEntranceEventArgs ev)
        {
            switch (CassieDestroyedLVL)
            {
                case 1:
                    Cassie.Message("pitch_1 .g6 pitch_0.4 . .g5 . . .g4 . . .g4 . . pitch_0.96  pitch_0.5 .g1 .g2 pitch_0.3 .g2 .g3 pitch_1. . pitch_0.9 pitch_0.2 .g5 .g4 pitch_1", true, false, false); ;
                    ev.IsAllowed = false;
                    break;
                case 3:
                    ev.IsAllowed = false;
                    Cassie.Message("pitch_1 .g6 pitch_0.4 . .g5", true, false, false);
                    break;
            }
        }
    }
}
