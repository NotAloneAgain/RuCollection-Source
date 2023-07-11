using CommandSystem;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RuCollection.API.Global
{
    public abstract class ObservableCommand : CommandWithData
    {
        static ObservableCommand()
        {
            LastUsed = new(Server.MaxPlayerCount);
        }

        public static Dictionary<Player, int> LastUsed { get; }

        public Player Executor { get; private set; }

        public override bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Executor = Player.Get(sender);

            if (Executor == null)
            {
                response = "Не получилось найти данные игрока, использующего команду.";
                return false;
            }

            LastUsed.SetOrAdd(Executor, Mathf.RoundToInt((float)Round.ElapsedTime.TotalSeconds));

            return base.Execute(arguments, sender, out response);
        }

        public override void Reset()
        {
            LastUsed.Clear();
        }
    }
}
