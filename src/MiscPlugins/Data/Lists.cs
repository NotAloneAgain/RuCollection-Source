using Exiled.API.Enums;
using PlayerRoles;
using Respawning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiscPlugins.Lists
{
    internal class Lists
    {
        public static List<RoleTypeId> RandomRoles = new List<RoleTypeId>()
        {
            RoleTypeId.ClassD,
            RoleTypeId.Scientist,
            RoleTypeId.FacilityGuard,
        };
        public static List<AmmoType> AmmoTypes = new List<AmmoType>()
        {
            AmmoType.Nato9,
            AmmoType.Ammo44Cal,
            AmmoType.Nato762,
            AmmoType.Ammo12Gauge,
            AmmoType.Nato556,
        };
        public static List<SpawnableTeamType> RandomSpawnableTeamType = new List<SpawnableTeamType>()
        {
            SpawnableTeamType.NineTailedFox,
            SpawnableTeamType.ChaosInsurgency,
        };
        public static List<ItemType> ItemTypeToDclass = new List<ItemType>()
        {
            ItemType.Flashlight,
            ItemType.KeycardJanitor,
            ItemType.KeycardScientist,
            ItemType.Coin,
            ItemType.Radio,
            ItemType.Painkillers,
        };
    }
}
