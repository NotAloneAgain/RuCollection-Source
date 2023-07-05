using CommandSystem;
using Exiled.API.Features;
using PlayerRoles;
using RuCollection.API;
using RuCollection.API.Subclasses.Group;
using RuCollection.API.Subclasses.Single;
using System;
using System.Collections.Generic;
using System.Linq;
namespace RuCollection.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public sealed class Betray : ICommand
    {
        public string Command { get; } = "betray";

        public string[] Aliases { get; } = Array.Empty<string>();

        public string Description { get; } = "Команда для предательства. Работает исключительно для скрытого подкласса.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count is not 0)
            {
                response = "Синтаксис команды: .betray";
                return false;
            }

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

            player.Role.Set(PlayerRoles.RoleTypeId.ChaosRifleman, PlayerRoles.RoleSpawnFlags.None);

            player.DisableEffect(Exiled.API.Enums.EffectType.Asphyxiated);
            player.Health = (player.Role.Base as IHealthbarRole).MaxHealth;

            response = "Вы успешно переоделись в повстанца хаоса";
            return true;
        }
    }
}
