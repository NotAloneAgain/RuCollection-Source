using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using System;
using PlayerEvent = Exiled.Events.Handlers.Player;
using ServerEvent = Exiled.Events.Handlers.Server;
using Exiled.API.Enums;
using Exiled.CustomRoles.API.Features;
using LevelSystem.API;
using PlayerRoles;
using System.Linq;
using MEC;

namespace LevelSystem.Handlers
{
    internal sealed class EventHandlers
    {
        public EventHandlers()
        {
            PlayerEvent.Verified += OnJoined;
            PlayerEvent.Dying += OnKill;
            ServerEvent.RoundEnded += OnRoundEnd;
            PlayerEvent.Escaping += OnEscape;
            ServerEvent.ReloadedRA += ReloadedRA;
        }
        public void UnsubscribeEvents()
        {
            PlayerEvent.Verified -= OnJoined;
            PlayerEvent.Dying -= OnKill;
            ServerEvent.RoundEnded -= OnRoundEnd;
            PlayerEvent.Escaping -= OnEscape;
            ServerEvent.ReloadedRA -= ReloadedRA;
        }
        public void OnJoined(VerifiedEventArgs ev)
        {
            Timing.CallDelayed(0.3f, () =>
            {
                ev.Player.GetLog();

                API.API.UpdateBadge(ev.Player, ev.Player.Group?.BadgeText);
            });
        }
        public void ReloadedRA()
        {
            foreach (Player pl in Player.List.Where(x => x.Group == null))
            {
                API.API.UpdateBadge(pl);
            }
        }
        public void OnKill(DyingEventArgs ev)
        {
            if (!ev.IsAllowed) return;

            if (ev.Player == null) return;
            if (ev.Player == ev.Attacker) return;

            Player killer = ev.DamageHandler.Type == DamageType.PocketDimension ? Player.Get(RoleTypeId.Scp106).FirstOrDefault() : ev.Attacker;
            if (killer == null) return;

            if (Plugin.Singleton.Config.KillXP.TryGetValue(ev.Player.Role.Type, out var PlayerKillXP))
            {
                var log = killer.GetLog();

                log.AddXP(PlayerKillXP);
                log.UpdateLog();
            }
            else
            {
                Log.Warn($"PlayerKillXP == null ({ev.Player.Role.Type})");
            }
        }

        public void OnEscape(EscapingEventArgs ev)
        {
            if (!ev.IsAllowed) return;

            //if (CustomRole.Get((uint)2).Check(ev.Player)) return;

            if (!Plugin.Singleton.Config.EscapeXP.TryGetValue(ev.Player.Role, out int xp))
            {
                Log.Warn($"No escape XP for {ev.Player.Role}");
                return;
            }

            var log = ev.Player.GetLog();
            log.AddXP(xp);
            log.UpdateLog();
        }

        public void OnRoundEnd(RoundEndedEventArgs ev)
        {
            Side team;
            switch (ev.LeadingTeam)
            {
                case LeadingTeam.FacilityForces:
                    team = Side.Mtf;
                    break;
                case LeadingTeam.ChaosInsurgency:
                    team = Side.ChaosInsurgency;
                    break;
                case LeadingTeam.Anomalies:
                    team = Side.Scp;
                    break;
                case LeadingTeam.Draw:
                    team = Side.None;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            foreach (var player in Player.Get(team))
            {
                var log = player.GetLog();
                if (log is null)
                    return;
                log.AddXP(Plugin.Singleton.Config.TeamWinXP);
                log.UpdateLog();
            }
        }
    }
}
