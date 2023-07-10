using CommandSystem;
using Exiled.API.Features;
using PlayerRoles;
using RuCollection.API.Global;
using RuCollection.API.Subclasses.Single;
using System;
using System.Collections.Generic;

namespace RuCollection.Commands
{
    public sealed class Betray : CustomCommand
    {
        public override string Command { get; } = "betray";

        public override string[] Aliases { get; } = Array.Empty<string>();

        public override string Description { get; } = "Команда для предательства. Работает исключительно для скрытого подкласса.";

        public override List<CommandType> Types { get; } = new List<CommandType>(1) { CommandType.PlayerConsole };

        public override bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
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

            player.Role.Set(RoleTypeId.ChaosRifleman, RoleSpawnFlags.None);

            player.DisableEffect(Exiled.API.Enums.EffectType.Asphyxiated);
            player.Health = player.MaxHealth;

            response = "Вы успешно переоделись в повстанца хаоса";
            return true;
        }
    }
}
