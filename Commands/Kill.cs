using CommandSystem;
using Exiled.API.Features;
using PlayerStatsSystem;
using System;
namespace NYPlugin.Commands
{
    internal class KILL
    {
        [CommandHandler(typeof(ClientCommandHandler))]
        public class KillCommand : ICommand
        {
            public string Command => "killme";

            public string[] Aliases => ["kill"];

            public string Description => "kill me";
            public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
            {
                if (Player.TryGet(sender, out Player player))
                {
                    player.Kill(new CustomReasonDamageHandler("自杀"));
                    response = "Ok";
                    return true;
                }
                response = "No";

                return false;
            }
        }
    }
}