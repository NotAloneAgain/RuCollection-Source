using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using Exiled.API.Features.Roles;
using MEC;
using PlayerRoles;
using PluginAPI.Commands;
using PluginAPI.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace RuCollection.API.Subclasses.Group
{
    public sealed class Informator : GroupSubclass
    {
        private static Informator _singleton;

        private Informator() : base(2) { }

        public static Informator Singleton => _singleton ??= new();

        public override string Name { get; } = "Информатор";

        public override RoleTypeId Role { get; } = RoleTypeId.FacilityGuard;

        public override string Message { get; } = "Вы - информатор!\nВладеет информацией о сбежавших SCP объектах (проверь консоль).";

        public override bool Show { get; } = true;



        public override Inventory Inventory { get; } = new Inventory(new(6)
        {
            { 0, new (1)
            {
                { ItemType.KeycardGuard, 100 },
            }
            },
            { 1, new (1)
            {
                { ItemType.GunFSP9, 100 },
            }
            },
            { 2, new (1)
            {
                {ItemType.Medkit, 100 },
            }
            },
            { 3, new (1)
            {
                { ItemType.GrenadeFlash, 100 },
            }
            },
            { 4, new (1)
            {
                { ItemType.Radio, 100 },
            }
            },
            { 5, new(1)
            {
                {ItemType.ArmorHeavy, 100 }
            }
            }
        });
        public override void Assign(Player player)
        {
            base.Assign(player);
            player.SendConsoleMessage($" {Player.Get(Team.SCPs)}", "red");
        }
    }
}
