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
    }
}
