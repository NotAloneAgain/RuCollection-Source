using CommandSystem;
using RuCollection.API.Global;
using RuCollection.Commands.Configs;
using System;
using System.Collections.Generic;

namespace RuCollection.Commands
{
    public sealed class Plugin : PluginWithData<Config>
    {
        public override string Name { get; } = "RuCollection.Commands";

        public override string Prefix { get; } = "RuCollection.Commands";

        public override string Author { get; } = ".grey#9120";

        public override Version Version { get; } = new(1, 0, 0);

        public new List<CustomCommand> Commands { get; } = new(20);

        public override void OnEnabled() { }

        public override void OnDisabled() { }

        public override void OnReloaded() { }

        public override void OnRegisteringCommands()
        {
            Commands.Add(new Force());
            Commands.Add(new Hide());
            Commands.Add(new Item());
            Commands.Add(new Size());
            Commands.Add(new Steal());
            Commands.Add(new Zombie());

            foreach (var command in Commands)
            {
                command.Subscribe();
            }

            base.OnEnabled();
        }

        public override void OnUnregisteringCommands()
        {
            Reset();

            foreach (var command in Commands)
            {
                command.Unsubscribe();
            }

            Commands.Clear();

            base.OnDisabled();
        }

        public override void Reset()
        {
            foreach (var command in Commands)
            {
                if (command is not CommandWithData dataCommand)
                {
                    return;
                }

                dataCommand.Reset();
            }
        }
    }
}
