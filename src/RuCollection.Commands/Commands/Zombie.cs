using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;
using RuCollection.API;
using RuCollection.API.Global;
using RuCollection.API.Subclasses.Single;
using System;
using System.Collections.Generic;

namespace RuCollection.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    internal sealed class Zombie : CustomCommand
    {
        public override string Command { get; } = "zombie";

        public override string[] Aliases { get; } = Array.Empty<string>();

        public override string Description { get; } = "Команда для становления зомби. Работает исключительно для секретного подкласса.";

        public override List<CommandType> Types { get; } = new List<CommandType>(1) { CommandType.PlayerConsole };

        public override bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!base.Execute(arguments, sender, out response))
            {
                return false;
            }

            Player player = Player.Get(sender);

            if (player == null)
            {
                response = "Не получилось найти данные игрока, использующего команду.";
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
