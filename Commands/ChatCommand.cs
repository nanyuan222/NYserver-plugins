using CommandSystem;
using Exiled.API.Features;
using MEC;
using RemoteAdmin;
using System;
using Hint = NYPlugin.Fuctions.Servers.Hint;
namespace NYPlugin.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Chat : ICommand
    {
        public string Command { get; } = "chat";
        public string[] Aliases { get; } = new string[] { "c" };
        public string Description { get; } = "队伍聊天";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is PlayerCommandSender plr)
            {
                Player player = Player.Get(plr.PlayerId);
                if (arguments.Count != 0)
                {
                    if (player.IsIntercomMuted == false)
                    {
                        foreach (var awaplayer in Player.Get(player.Role.Team))
                        {
                            Hint.AddTempHint(awaplayer, "<align=right><size=20>[队伍聊天]" + player.Role.Type + " " + player.Nickname + ":" + string.Join(" ", arguments) + "</size></align>", 10);
                        }
                    }
                    else
                    {
                        response = "你被禁言了联系管理接触禁止";
                        return false;
                    }
                }
                else
                {
                    response = "请输入内容";
                    return false;
                }
            }
            response = "完成(*/ω＼*)";
            return false;
        }
    }
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Bchat : ICommand
    {
        public string Command { get; } = "bchat";
        public string[] Aliases { get; } = new string[] { "bc" };
        public string Description { get; } = "全体聊天";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is PlayerCommandSender plr)
            {
                Player player = Player.Get(plr.PlayerId);
                if (arguments.Count != 0)
                {
                    if (player.IsIntercomMuted == false)
                    {
                        Timing.RunCoroutine(Hint.ChatTiming(player, string.Join(" ", arguments)));
                        response = "发送成功";
                        return true;
                    }
                    else
                    {
                        response = "你被禁言了联系管理接触禁止";
                        return false;
                    }
                }
                else
                {
                    response = "请输入内容";
                    return false;
                }
            }
            response = "完成ヾ(≧▽≦*)o";
            return false;
        }
    } 
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Tps : ICommand
    {
        public string Command => "tps";

        public string[] Aliases => Array.Empty<string>();

        public string Description => "show server tps";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = $"服务器TPS:{Server.Tps}/60\n服务器版本:{Server.Version}\nCiallo～(∠・ω< )⌒★";
            return true;
        }
    }
}

