using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            foreach (var item in player.Items)
            {
                if (item.IsKeycard)
                {
                    continue;
                }

                player.DropItem(item);
            }
        }

        public static string GetSecondsString(this double seconds)
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
