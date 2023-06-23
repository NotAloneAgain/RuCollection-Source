using CommandSystem;
using Exiled.API.Features;
using RuCollection.API;
using RuCollection.API.Subclasses.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace RuCollection.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public sealed class Steal : ICommand
    {
        private const string Spalili = "<line-height=95%><size=95%><voffset=-20em><color=#BC5D58>Вы услышали как что-то шуршит в ваших карманах... {0} выглядит подозрительным...</color></size></voffset>";
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

        public string Command { get; } = "steal";

        public string[] Aliases { get; } = Array.Empty<string>();

        public string Description { get; } = "Команда для воровства. Работает исключительно для карманника и профессионального вора.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

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

            var items = target.Items.Where(item => !_banned.Contains(item.Type)).Select(x => x.Type).Distinct();

            if (items.Count() == 0)
            {
                response = "Вы обнаружили только пустые карманы";
                return false;
            }

            player.SendConsoleMessage("Мням, у него определенно что-то есть. Спиздим!", "yellow");

            ItemType targetItem = items.ElementAt(Random.Range(0, items.Count()));

            player.SendConsoleMessage($"Мням, нашей целью будет {targetItem}...", "yellow");

            int failureChance = (isPickpocket, isThief) switch
            {
                (true, false) => 88,
                (false, true) => 92
            };

            double seconds = (DateTime.Now - _used[player]).TotalSeconds;

            if (_used.ContainsKey(player) && seconds <= 30)
            {
                response = $"Вам осталось ждать {GetSecondsString(seconds)} до следующей попытки.";
                return false;
            }

            if (!_used.ContainsKey(player))
            {
                _used.Add(player, DateTime.Now);
            }
            else
            {
                _used[player] = DateTime.Now;
            }

            if (Random.Range(0, 101) >= failureChance)
            {
                response = "Мням, спиздец не удалось, нам пиздец. Быстрые ноги пизды не получааааааааааааааааааааааааааааат....";

                target.ShowHint(string.Format(Spalili, player.CustomName), 10);

                return false;
            }

            player.AddItem(targetItem);
            target.RemoveItem(target.Items.First(item => item.Type == targetItem));

            response = "Мням, спиздили.";

            failureChance = (isPickpocket, isThief) switch
            {
                (true, false) => 50,
                (false, true) => 60
            };

            if (Random.Range(0, 101) >= failureChance)
            {
                target.ShowHint(string.Format(Spalili, player.CustomName), 10);
            }

            return false;
        }

        private static string GetSecondsString(double seconds)
        {
            int secondsInt = (int)seconds;
            string secondsString = secondsInt switch
            {
                int n when n % 100 >= 11 && n % 100 <= 14 => "секунд",
                int n when n % 10 == 1 => "секунда",
                int n when n % 10 >= 2 && n % 10 <= 4 => "секунды",
                _ => "секунд"
            };

            return secondsInt.ToString() + " " + secondsString;
        }
    }
}
