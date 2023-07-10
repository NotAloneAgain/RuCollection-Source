using CommandSystem;
using PluginAPI.Commands;
using RemoteAdmin;
using System;
using System.Collections.Generic;

namespace RuCollection.API.Global
{
    public abstract class CustomCommand : ICommand
    {
        public abstract string Command { get; }

        public abstract string[] Aliases { get; }

        public abstract string Description { get; }

        public abstract List<CommandType> Types { get; }

        public abstract bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response);

        public void Subscribe()
        {
            foreach (var type in Types)
            {
                switch (type)
                {
                    case CommandType.RemoteAdmin:
                        {
                            CommandProcessor.RemoteAdminCommandHandler.RegisterCommand(this);

                            break;
                        }
                    case CommandType.PlayerConsole:
                        {
                            GameCore.Console.singleton.ConsoleCommandHandler.RegisterCommand(this);

                            break;
                        }
                    case CommandType.ServerConsole:
                        {
                            QueryProcessor.DotCommandHandler.RegisterCommand(this);

                            break;
                        }
                }
            }
        }

        public void Unsubscribe()
        {

        }
    }
}
