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
    public sealed class Steal : CommandWithData
    {
        private static List<ItemType> _banned;
        private static Dictionary<Player, DateTime> _used;

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

            _used = new();
        }

        public override string Command { get; } = "steal";

        public override string[] Aliases { get; } = Array.Empty<string>();

        public override string Description { get; } = "Команда для воровства. Работает исключительно для карманника и профессионального вора.";

        public override List<CommandType> Types { get; } = new List<CommandType>(1) { CommandType.PlayerConsole };

        public override bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (player == null)
            {
                response = "Не получилось найти данные игрока, использующего команду.";
                return false;
            }

            bool isThief = Thief.Singleton.Players.Contains(player);
            bool isPickpocket = Pickpocket.Singleton.Players.Contains(player);

            if (!isThief && !isPickpocket || player.IsCuffed || player.IsInventoryFull)
            {
                response = "Ты не можешь использовать эту команду!";
                return false;
            }

            if (arguments.Count is not 0)
            {
                response = "Синтаксис команды: .steal";
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

            player.SendConsoleMessage("Мням, у него определенно что-то есть. Спиздим!", "yellow");

            ItemType targetItem = items.ElementAt(Random.Range(0, items.Count()));

            player.SendConsoleMessage($"Мням, нашей целью будет {targetItem}...", "yellow");

            int failureChance = (isPickpocket, isThief) switch
            {
                (false, true) => 92,
                _ => 88
            };

            var now = DateTime.Now;

            if (_used.TryGetValue(player, out var time))
            {
                double seconds = (now - time).TotalSeconds;

                if (seconds <= 30)
                {
                    response = $"Вам осталось ждать {(30 - seconds).GetSecondsString()} до следующей попытки.";
                    return false;
                }

                _used[player] = now;
            }
            else
            {
                _used.Add(player, DateTime.Now);
            }

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

        public override void Reset()
        {
            _used.Clear();
        }
    }
}
