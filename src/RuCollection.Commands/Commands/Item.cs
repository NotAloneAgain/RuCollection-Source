using CommandSystem;
using Exiled.API.Extensions;
using Exiled.API.Features;
using RuCollection.API.Global;
using RuCollection.API.Subclasses.Single;
using System;
using System.Collections.Generic;

namespace RuCollection.Commands
{
    public sealed class Item : CustomCommand
    {
        public override string Command { get; } = "item";

        public override string[] Aliases { get; } = Array.Empty<string>();

        public override string Description { get; } = "Команда для выдачи себе предмета. Работает исключительно для SCP-343.";

        public override List<CommandType> Types { get; } = new List<CommandType>(1) { CommandType.PlayerConsole };

        public override bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (player == null)
            {
                response = "Не получилось найти данные игрока, использующего команду.";
                return false;
            }

            bool isGod = Scp343.Singleton.Player == player;

            if (!isGod)
            {
                response = "Ты не можешь использовать эту команду!";
                return false;
            }

            if (arguments.Count != 1 || !int.TryParse(arguments.At(0), out int id))
            {
                response = "Синтаксис команды: .item [ID]";
                return false;
            }

            ItemType item = (ItemType)id;

            if (item.GetCategory() is ItemCategory.Firearm or ItemCategory.Grenade or ItemCategory.SCPItem or ItemCategory.MicroHID or ItemCategory.Ammo or ItemCategory.Armor || item is ItemType.Jailbird)
            {
                item = ItemType.Medkit;
                player.SendConsoleMessage("Атятя, какой плахой мальчик, хотел пушку, ладно, держи аптеку.", "red");
            }

            player.AddItem(item);

            response = "Успешно!";
            return true;
        }
    }
}
