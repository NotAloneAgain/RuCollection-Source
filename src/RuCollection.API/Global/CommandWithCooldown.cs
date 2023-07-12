using CommandSystem;
using Exiled.API.Features;
using System;
using UnityEngine;

namespace RuCollection.API.Global
{
    public abstract class CommandWithCooldown : ObservableCommand
    {
        public abstract int Cooldown { get; }

        public override bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!base.Execute(arguments, sender, out response))
            {
                return false;
            }

            var time = Mathf.RoundToInt((float)(Round.ElapsedTime.TotalSeconds - (LastUsed?[Executor] ?? 0)));

            if (time <= Cooldown)
            {
                response = $"Вам осталось ждать {time.GetSecondsString()} до следующей попытки.";
                return false;
            }

            return true;
        }
    }
}
