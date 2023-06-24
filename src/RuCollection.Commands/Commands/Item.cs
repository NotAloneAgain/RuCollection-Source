using CommandSystem;
using Exiled.API.Extensions;
using Exiled.API.Features;
using RuCollection.API.Subclasses.Single;
using System;

namespace RuCollection.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public sealed class Item : ICommand
    {
        public string Command { get; } = "item";

        public string[] Aliases { get; } = Array.Empty<string>();

        public string Description { get; } = "Команда для выдачи себе предмета. Работает исключительно для SCP-343.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            bool isGod = Scp343.Singleton.Player == player;

            if (!isGod)
            {
                response = "Ты не можешь использовать эту команду!";
                return false;
            }

            if (arguments.Count is not 1 || !int.TryParse(arguments.At(0), out int id))
            {
                response = "Синтаксис команды: .item [ID]";
                return false;
            }

            ItemType item = (ItemType)id;

            if (Scp343.IsDangerous(item))
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
