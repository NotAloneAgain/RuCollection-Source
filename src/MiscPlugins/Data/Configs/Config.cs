using System.ComponentModel;
using Exiled.API.Interfaces;

namespace MiscPlugins.Configs
{
    public sealed class Config : IConfig
    {
        [Description("Enabled or not.")]
        public bool IsEnabled { get; set; } = true;
        // FullRoundRestart
        [Description("Debug enabled or not.")]
        public bool Debug { get; set; } = false;
        [Description("Do server fully restart after ending round or not.")]
        public bool FullRoundRestart { get; set; } = true;
        [Description("Do loot spawn in rooms without opening door (LczArmory, HczArmory) or not.")]
        public bool RoomLootSpawn { get; set; } = true;
    }
}
