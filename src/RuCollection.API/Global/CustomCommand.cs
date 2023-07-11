using CommandSystem;
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

        public virtual int MaxArguments { get; } = 0;

        public virtual string UsingExample { get; } = string.Empty;

        public virtual bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count != MaxArguments)
            {
                response = GetSyntax();
                return false;
            }

            response = "Успешно!";
            return true;
        }

        public virtual string GetSyntax() => $"Синтаксис команды: .{Command} {UsingExample}";

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
                            QueryProcessor.DotCommandHandler.RegisterCommand(this);

                            break;
                        }
                    case CommandType.ServerConsole:
                        {
                            GameCore.Console.singleton.ConsoleCommandHandler.RegisterCommand(this);

                            break;
                        }
                }
            }
        }

        public void Unsubscribe()
        {
            foreach (var type in Types)
            {
                switch (type)
                {
                    case CommandType.RemoteAdmin:
                        {
                            CommandProcessor.RemoteAdminCommandHandler.UnregisterCommand(this);

                            break;
                        }
                    case CommandType.PlayerConsole:
                        {
                            QueryProcessor.DotCommandHandler.UnregisterCommand(this);

                            break;
                        }
                    case CommandType.ServerConsole:
                        {
                            GameCore.Console.singleton.ConsoleCommandHandler.UnregisterCommand(this);

                            break;
                        }
                }
            }
        }
    }
}
