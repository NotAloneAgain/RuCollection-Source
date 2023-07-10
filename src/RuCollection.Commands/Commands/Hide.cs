﻿using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using RuCollection.API;
using RuCollection.API.Global;
using RuCollection.API.Subclasses.Single;
using System;
using System.Collections.Generic;

namespace RuCollection.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public sealed class Hide : ICommand, IHasData
    {
        private static Dictionary<Player, DateTime> _used;

        static Hide()
        {
            _used = new();
        }

        public string Command { get; } = "hide";

        public string[] Aliases { get; } = Array.Empty<string>();

        public string Description { get; } = "Команда для сокрытия. Работает исключительно для скрытого подкласса.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
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

            if (arguments.Count is not 0)
            {
                response = "Синтаксис команды: .hide";
                return false;
            }

            var now = DateTime.Now;

            if (_used.TryGetValue(player, out var time))
            {
                double seconds = (now - time).TotalSeconds;

                if (seconds <= 180)
                {
                    response = $"Вам осталось ждать {(180 - seconds).GetSecondsString()} до следующей попытки.";
                    return false;
                }

                _used[player] = now;
            }
            else
            {
                _used.Add(player, DateTime.Now);
            }

            player.EnableEffect(EffectType.Invisible, 20);

            response = "Успешно!";
            return true;
        }

        public void Reset()
        {
            _used.Clear();
        }
    }
}
