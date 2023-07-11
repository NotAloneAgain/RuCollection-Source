using CommandSystem;
using Exiled.API.Features;
using RuCollection.API.Global;
using RuCollection.API.Subclasses.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RuCollection.Commands
{
    public sealed class Size : CustomCommand
    {
        public override string Command { get; } = "size";

        public override string[] Aliases { get; } = Array.Empty<string>();

        public override string Description { get; } = "Команда для смены размера. Работает исключительно для SCP-343 и админов.";

        public override List<CommandType> Types { get; } = new List<CommandType>(3) { CommandType.PlayerConsole, CommandType.RemoteAdmin, CommandType.ServerConsole };

        public override int MaxArguments { get; } = 4;

        public override string UsingExample { get; } = "[PlayerId/0/me/all] [3D Vector]";

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

            bool isGod = Scp343.Singleton.Player == player;

            if (!isGod && !player.RemoteAdminAccess)
            {
                response = "Ты не можешь использовать эту команду!";
                return false;
            }

            List<Player> targets = arguments.At(0) switch
            {
                "0" or "me" => new(1) { player },
                "all" => Player.List.ToList(),
                _ => new(1) { Player.Get(int.Parse(arguments.At(0))) },
            };

            if (isGod && targets.Any(p => Scp343.Singleton.Player != p))
            {
                response = "Ты можешь сменить размер только себе.";
                return false;
            }

            if (!float.TryParse(arguments.At(1), out float x) || !float.TryParse(arguments.At(2), out float y) || !float.TryParse(arguments.At(3), out float z))
            {
                response = "Не удалось пропарсить размер";
                return false;
            }

            if (isGod && (Math.Round(x, 1) is > 1.4 or < 0.2 || Math.Round(y, 1) is > 1.4 or < 0.2 || Math.Round(z, 1) is > 1.4 or < 0.2))
            {
                response = "Слишком большой размер";
                return false;
            }

            Vector3 size = new(x, y, z);

            targets.ForEach(ply => ply.Scale = size);

            response = "Успешно!";
            return true;
        }
    }
}
