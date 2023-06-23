using RuCollection.Commands.Configs;
using System;

namespace RuCollection.Commands
{
    public sealed class Plugin : Exiled.API.Features.Plugin<Config>
    {
        public override string Name { get; } = "RuCollection.Commands";

        public override string Prefix { get; } = "RuCollection.Commands";

        public override string Author { get; } = ".grey#9120";

        public override Version Version { get; } = new(1, 0, 0);

        public override void OnReloaded() { }
    }
}
