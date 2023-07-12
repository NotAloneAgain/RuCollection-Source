using CommandSystem;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RuCollection.API.Global
{
    public abstract class ObservableCommand<TObject> : CommandWithData
    {
        static ObservableCommand()
        {
            LastUsed = new(Server.MaxPlayerCount);
        }

        public static Dictionary<Player, TObject> LastUsed { get; }

        public Player Executor { get; private set; }

        public override bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Executor = Player.Get(sender);

            if (Executor == null)
            {
                response = "Не получилось найти данные игрока, использующего команду.";
                return false;
            }

            LastUsed.SetOrAdd(Executor, GetValue());

            return base.Execute(arguments, sender, out response);
        }

        public abstract TObject GetValue();

        public override void Reset()
        {
            LastUsed.Clear();
        }
    }
}
