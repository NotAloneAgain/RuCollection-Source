using Exiled.Events.EventArgs.Player;
using Exiled.API.Enums;
using RuCollection.API.ScpSwap;

namespace ScpSwap.Handlers
{
    internal sealed class PlayerHandlers
    {
        private readonly string _text;
        private readonly float _duration;

        internal PlayerHandlers(string text, float duration)
        {
            _text = text;
            _duration = duration;
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Reason == SpawnReason.RoundStart)
            {
                ev.Player.ShowHint(string.Format(_text, Swap.SwapDuration), _duration);
            }
        }
    }
}
