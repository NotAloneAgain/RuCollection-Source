using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using RuCollection.API.Global;
using RuCollection.API.Subclasses.Single;
using System;
using System.Collections.Generic;

namespace RuCollection.Commands
{
    public sealed class Hide : CommandWithCooldown
    {
        public override string Command { get; } = "hide";

        public override string[] Aliases { get; } = Array.Empty<string>();

        public override string Description { get; } = "Команда для сокрытия. Работает исключительно для скрытого подкласса.";

        public override List<CommandType> Types { get; } = new List<CommandType>(1) { CommandType.PlayerConsole };

        public override int Cooldown { get; } = 180;

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

            if (Hidden.Singleton.Player != player)
            {
                response = "Ты не можешь использовать эту команду!";
                return false;
            }

            player.EnableEffect(EffectType.Invisible, 20);

            response = "Успешно!";
            return true;
        }
    }
}
