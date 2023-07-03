using CommandSystem;
using Exiled.API.Features;
using RuCollection.API;
using RuCollection.API.Subclasses.Group;
using RuCollection.API.Subclasses.Single;
using System;
using System.Collections.Generic;
using System.Linq;
namespace RuCollection.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public sealed class Rifleman : ICommand
    { 

        public string Command { get; } = "rifleman";

        public string[] Aliases { get; } = { "rifle" };

        public string Description { get; } = "Команда для переодевания в повстанца хаоса.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (player == null)
            {
                response = "Не получилось найти данные игрока, использующего команду.";
                return false;
            }

            bool isImposter = Imposter.Singleton.Players.Contains(player);

            if (!isImposter || player.IsCuffed)
            {
                response = "Ты не можешь использовать эту команду!";
                return false;
            }

            if (arguments.Count is not 0)
            {
                response = "Синтаксис команды: .rifleman";
                return false;
            }
            player.Role.Set(PlayerRoles.RoleTypeId.ChaosRifleman, PlayerRoles.RoleSpawnFlags.None);
            player.DisableEffect(Exiled.API.Enums.EffectType.Asphyxiated);
            response = "Вы успешно переоделись в повстанца хаоса";
            return true;
        }
    }
}
