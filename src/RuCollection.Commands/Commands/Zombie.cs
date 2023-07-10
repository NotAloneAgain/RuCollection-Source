using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;
using RuCollection.API;
using RuCollection.API.Subclasses.Single;
using System;

namespace RuCollection.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    internal sealed class Zombie : ICommand
    {
        public string Command { get; } = "zombie";

        public string[] Aliases { get; } = Array.Empty<string>();

        public string Description { get; } = "Команда для становления зомби. Работает исключительно для секретного подкласса.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (player == null)
            {
                response = "Не получилось найти данные игрока, использующего команду.";
                return false;
            }

            if (arguments.Count != 0)
            {
                response = "Синтаксис команды: .zombie";
                return false;
            }

            if (player.Role.Type != RoleTypeId.Scientist || Infected.Singleton.Player != player)
            {
                response = "Вы не подходите требованиям команды.";
                return false;
            }

            player.DropAllWithoutKeycard();
            player.Role.Set(RoleTypeId.Scp0492, SpawnReason.Revived, RoleSpawnFlags.None);

            response = "Вы стали зомби!";
            return true;
        }
    }
}
