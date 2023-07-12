using CommandSystem;
using Exiled.API.Features;
using RuCollection.API;
using RuCollection.API.Global;
using RuCollection.API.Subclasses.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace RuCollection.Commands
{
    public sealed class Steal : CommandWithCooldown
    {
        private static List<ItemType> _banned;

        static Steal()
        {
            _banned = new()
            {
                 ItemType.ParticleDisruptor,
                 ItemType.MicroHID,
                 ItemType.GunLogicer,
                 ItemType.ArmorCombat,
                 ItemType.ArmorHeavy,
                 ItemType.ArmorLight,
                 ItemType.GunShotgun,
                 ItemType.GunCom45,
                 ItemType.GunFSP9,
                 ItemType.GunE11SR,
                 ItemType.GunCrossvec,
                 ItemType.SCP244a,
                 ItemType.SCP244b
            };
        }

        public override string Command { get; } = "steal";

        public override string[] Aliases { get; } = Array.Empty<string>();

        public override string Description { get; } = "Команда для воровства. Работает исключительно для карманника и профессионального вора.";

        public override List<CommandType> Types { get; } = new List<CommandType>(1) { CommandType.PlayerConsole };

        public override short Cooldown { get; } = 30;

        public override bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!base.Execute(arguments, sender, out response))
            {
                return false;
            }

            Player player = Executor;

            bool isThief = Thief.Singleton.Players?.Contains(player) ?? false;
            bool isPickpocket = Pickpocket.Singleton.Players?.Contains(player) ?? false;
            bool isSubclassed = isThief || isPickpocket;

            if (!isSubclassed || player.IsCuffed || player.IsInventoryFull)
            {
                response = "Ты не можешь использовать эту команду!";
                return false;
            }

            Player target = Methods.GetFromView(player);

            if (target == null || target == player || !target.IsAlive)
            {
                response = "Цель нераспознана";
                return false;
            }

            if (target.IsInventoryEmpty)
            {
                response = "Вы обнаружили только пустые карманы";
                return false;
            }

            var items = target.Items.Where(item => !_banned.Contains(item.Type)).Select(x => x.Type).Distinct();

            if (items.Count() == 0)
            {
                response = "Вы не обнаружили предметы, которые можете украсть.";
                return false;
            }

            player.SendConsoleMessage("Мням, у него определенно что-то есть. Спиздим!", "yellow");

            ItemType targetItem = items.ElementAt(Random.Range(0, items.Count()));

            player.SendConsoleMessage($"Мням, нашей целью будет {targetItem}...", "yellow");

            int failureChance = (isPickpocket, isThief) switch
            {
                (false, true) => 92,
                _ => 88
            };

            if (Random.Range(0, 101) >= failureChance)
            {
                response = "Мням, спиздец не удалось, нам пиздец. Быстрые ноги пизды не получааааааааааааааааааааааааааааат....";

                target.ShowHint(string.Format(StringConstants.StealFailed, player.CustomName), 10);

                return false;
            }

            player.AddItem(targetItem);
            target.RemoveItem(target.Items.First(item => item.Type == targetItem));

            response = "Мням, спиздили.";

            failureChance = (isPickpocket, isThief) switch
            {
                (false, true) => 60,
                _ => 50
            };

            if (Random.Range(0, 101) >= failureChance)
            {
                target.ShowHint(string.Format(StringConstants.StealFailed, player.CustomName), 10);
            }

            return true;
        }
    }
}
