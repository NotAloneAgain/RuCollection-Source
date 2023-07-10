﻿using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using PlayerRoles;
using RuCollection.API.Global;
using RuCollection.API.ScpSwap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RuCollection.Commands
{
    internal sealed class Force : CommandWithData
    {
        private static Dictionary<Player, bool> _forced;

        static Force()
        {
            _forced = new(8);
        }

        public override string Command { get; } = "force";

        public override string[] Aliases { get; } = Array.Empty<string>();

        public override string Description { get; } = "Команда для смены своего класса. Работает исключительно для SCP-Объектов.";

        public override List<CommandType> Types { get; } = new List<CommandType>(1) { CommandType.PlayerConsole };

        public override bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count != 1)
            {
                response = "Синтаксис команды: .force [Номер]";
                return false;
            }

            Player player = Player.Get(sender);

            if (player == null)
            {
                response = "Не получилось найти данные игрока, использующего команду.";
                return false;
            }

            string number = arguments.At(0);

            if (string.IsNullOrEmpty(number) || !ushort.TryParse(number, out ushort scp))
            {
                response = "Синтаксис команды: .force [Номер]";
                return false;
            }

            if (Round.ElapsedTime.TotalSeconds > Swap.SwapDuration)
            {
                response = $"Прошло более {Swap.SwapDuration} секунд после начала раунда.";
                return false;
            }

            if (!(Swap.AllowedScps?.Contains(player.Role) ?? false))
            {
                response = "Вы не подходите требованиям команды.";
                return false;
            }

            if (Swap.Prevent && (_forced?.TryGetValue(player, out bool forced) ?? false) && forced)
            {
                response = "Сменить роль можно лишь один раз.";
                return false;
            }

            RoleTypeId role = scp switch
            {
                49 => RoleTypeId.Scp049,
                79 => RoleTypeId.Scp079,
                96 => RoleTypeId.Scp096,
                106 => RoleTypeId.Scp106,
                173 => RoleTypeId.Scp173,
                939 => RoleTypeId.Scp939,
                _ => RoleTypeId.None,
            };

            if (role == RoleTypeId.None)
            {
                response = "Не удалось найти такой SCP-Объект.";
                return false;
            }

            if (Player.Get(role).Count() == Swap.Slots[role])
            {
                response = "Все слоты за данный объект заняты.";
                return false;
            }

            if (role == player.Role.Type)
            {
                response = "Вы и так данный SCP";
                return false;
            }

            player.Role.Set(role, SpawnReason.ForceClass, RoleSpawnFlags.All);

            response = "Вы сменили свой SCP-Объект!";

            _forced.Add(player, true);

            Timing.CallDelayed((float)(Swap.SwapDuration - Round.ElapsedTime.TotalSeconds), delegate
            {
                _forced.Remove(player);
            });

            player.SendConsoleMessage($"Желаем удачной игры за SCP-{role.ToString().Substring(3)}", "yellow");

            return true;
        }

        public override void Reset()
        {
            _forced.Clear();
        }
    }
}
