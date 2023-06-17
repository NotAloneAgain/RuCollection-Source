using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;
using RuCollection.API.ScpSwap;
using System;
using System.Linq;

namespace ScpSwap.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    internal sealed class Force : ICommand
    {
        public string Command { get; } = "force";

        public string[] Aliases { get; } = Array.Empty<string>();

        public string Description { get; } = "Команда для смены своего класса. Работает исключительно для SCP-Объектов.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (arguments.Count < 1)
            {
                response = "Использование .force [номер SCP]";
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
                response = $"Прошло более {Swap.SwapDuration} секунд после начала раунда..";
                return false;
            }

            if (!Swap.AllowedScps.Contains(player.Role))
            {
                response = "Вы не подходите требованиям команды.";
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

            player.Role.Set(role, SpawnReason.ForceClass, RoleSpawnFlags.All);

            response = "Вы сменили свой SCP-Объект!";

            player.SendConsoleMessage($"Желаем удачной игры за SCP-{role.ToString().Substring(3)}", "yellow");

            return true;
        }
    }
}
