﻿using Exiled.API.Features;
using System.Collections.Generic;
using UnityEngine;

namespace RuCollection.API
{
    public static class Methods
    {
        public static Player GetFromView(Player owner)
        {
            if (!Physics.Raycast(owner.Position, owner.Transform.forward, out var hit, 5))
            {
                return null;
            }

            Player target = Player.Get(hit.transform.GetComponentInParent<ReferenceHub>());

            return target;
        }

        public static void DropAllWithoutKeycard(this Player player)
        {
            if (player.IsInventoryEmpty)
            {
                return;
            }

            foreach (var item in player.Items)
            {
                if (item.IsKeycard)
                {
                    continue;
                }

                player.DropItem(item);
            }
        }

        public static string GetSecondsString(this double seconds) => Mathf.RoundToInt((float)seconds).GetSecondsString();

        public static string GetSecondsString(this int seconds)
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

        public static void SetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue element, bool rewrite)
        {
            if (dictionary.ContainsKey(key))
            {
                if (rewrite)
                {
                    dictionary[key] = element;
                }

                return;
            }

            dictionary.Add(key, element);
        }
    }
}
