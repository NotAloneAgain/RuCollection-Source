using CommandSystem;
using Exiled.API.Features;
using System;
using UnityEngine;

namespace RuCollection.API.Global
{
    public abstract class CommandWithCooldown : ObservableCommand<int>
    {
        public abstract short Cooldown { get; }

        public sealed override bool RewriteLast { get; } = false;

        public override bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!base.Execute(arguments, sender, out response))
            {
                return false;
            }

            var player = Executor;

            var time = Mathf.RoundToInt((float)Round.ElapsedTime.TotalSeconds) - LastUsed[player];

            if (time >= Cooldown)
            {
                response = $"Вам осталось ждать {time.GetSecondsString()} до следующей попытки.";
                return false;
            }

            LastUsed[player] = GetValue();

            return true;
        }

        public override int GetValue() => Mathf.RoundToInt((float)Round.ElapsedTime.TotalSeconds);
    }
}
