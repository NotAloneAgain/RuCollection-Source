﻿using System.ComponentModel;
using Exiled.API.Interfaces;

namespace LevelSystem.Configs
{
    public sealed class Config : IConfig
    {
        [Description("Enabled or not.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Debug enabled or not.")]
        public bool Debug { get; set; } = false;
    }
}
