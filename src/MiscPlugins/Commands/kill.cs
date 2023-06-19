namespace MiscPlugins.Commands
{
    using System;
    using CommandSystem;
    using Exiled.API.Features;
    [CommandHandler(typeof(ClientCommandHandler))]
    public class kill : ICommand
    {
        public string Command { get; } = "kill";
        public string[] Aliases { get; } = new string[] { };
        public string Description { get; } = "<b>Команда для самоубийства..</b>";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if(!player.IsAlive)
            {
                response = "Чоо....";
                return false;
            }

            player.Kill("вскрыл вены..");

            response = "Успешно?";
            return true;
        }
    }
}