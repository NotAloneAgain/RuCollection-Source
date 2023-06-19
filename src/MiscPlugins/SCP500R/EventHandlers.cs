using Exiled.Events.EventArgs.Player;
using MiscPlugins.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiscPlugins.SCP500R.Handlers
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Exiled.Events.Handlers.Player.Left += OnLeft;
        }
        ~EventHandlers()
        {
            Exiled.Events.Handlers.Player.Left -= OnLeft;
        }
        private void OnLeft(LeftEventArgs ev)
        {
            ev.Player.IsOverwatchEnabled = false;

            if (Res.DiedWithSCP500R.Count == 0) return;

            if (ev.Player == Res.DiedWithSCP500R.First())
            {
                Res.DiedWithSCP500R.Clear();
                Res.StatusEffectBase.Clear();
                Res.RoleDiedWithSCP500R.Clear();
            }
        }
    }
}
