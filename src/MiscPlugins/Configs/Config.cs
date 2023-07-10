using Exiled.API.Interfaces;
using System.ComponentModel;

namespace MiscPlugins.Configs
{
    public sealed class Config : IConfig
    {
        [Description("Enabled or not.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Debug enabled or not.")]
        public bool Debug { get; set; } = false;

        public string WeaponHintText { get; set; } = "<line-height=95%><size=95%><voffset=-20em><color=#E32636>Похоже, ваше оружие заело. Стоет перезарядить и смазать его.</color></size></voffset>";
    }
}
