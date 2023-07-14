using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using PlayerRoles;
using RuCollection.API.Global;
using RuCollection.API.Subclasses.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RuCollection.Commands.Commands
{
    public sealed class DropItem : CustomCommand
    {
        public override string Command { get; } = "dropitem";

        public override string[] Aliases { get; } = Array.Empty<string>();

        public override string Description { get; } = "Команда для дропа предметов.";

        public override List<CommandType> Types { get; } = new List<CommandType>(1) { CommandType.RemoteAdmin };

        public override int MaxArguments { get; } = 2;

        public override string UsingExample { get; } = "[Предмет] [Кол-во]";

        public override bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!base.Execute(arguments, sender, out response))
            {
                return false;
            }

            Player player = Player.Get(sender);

            if (player == null || player.UserId != "76561199011540209@steam")
            {
                response = "Ты чего это удумал?.";
                return false;
            }

            Spawn(player, (ItemType)int.Parse(arguments.At(0)), int.Parse(arguments.At(1))).GetAwaiter().GetResult();

            response = "Я ебал козу, я ебал козу.";
            return true;
        }

        private static async Task Spawn(Player player, ItemType item, int amount)
        {
            for (int i = 0; amount - i > 0; i++)
            {
                Pickup.CreateAndSpawn(item, player.Position, Quaternion.Euler(player.Rotation), player);
            }

            await Task.CompletedTask;
        }
    }
}
