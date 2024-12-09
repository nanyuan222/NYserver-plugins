using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using Hints;
using MEC;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NYPlugin;
//©版权归小鲨鱼｜一只小杂鱼｜所有
namespace NYPlugin.Fuctions.Servers
{
    public class Awa
    {
        private Player player;
        private int playerid;
        private bool wait2;
        private List<string> strings = new();
        public Player Playerawa { get => player; set => player = value; }
        public int Playeridawa { get => playerid; set => playerid = value; }
        public List<string> Message { get => strings; set => strings = value; }
        public bool Wait { get => wait2; set => wait2 = value; }
    }
    public class Hint
    {
        private static readonly List<CoroutineHandle> Coroutines = new();
        private static readonly List<string> chatList = new();
        private static string scphpinfo;
        public static List<Awa> awas = new();
        public static bool showchat;
        public static void GetSCPHP()
        {
            int scp492num = 0;
            foreach (var player in Player.Get(Team.SCPs))
            {
                try
                {
                    if (player.Role == RoleTypeId.Scp106)
                    {
                        scphpinfo = scphpinfo + "\n<color=#FFA500>SCP106</color>[<color=#FFFF00>" + player.Health + "/" + player.MaxHealth + "</color>][AHP:<color=#FF0000>" + player.HumeShield + "</color>][区域:<color=#FF0000>" + APIs.NYAPI.区域[player.Zone] + "</color>]";
                    }
                    if (player.Role == RoleTypeId.Scp939)
                    {
                        scphpinfo = scphpinfo + "\n<color=#FFA500>SCP939</color>[<color=#FFFF00>" + player.Health + "/" + player.MaxHealth + "</color>][AHP:<color=#FF0000>" + player.HumeShield + "</color>][区域:<color=#FF0000>" + APIs.NYAPI.区域[player.Zone] + "</color>]";
                    }
                    if (player.Role == RoleTypeId.Scp173)
                    {
                        scphpinfo = scphpinfo + "\n<color=#FFA500>SCP173</color>[<color=#FFFF00>" + player.Health + "/" + player.MaxHealth + "</color>][AHP:<color=#FF0000>" + player.HumeShield + "</color>][区域:<color=#FF0000>" + APIs.NYAPI.区域[player.Zone] + "</color>]";
                    }
                    if (player.Role == RoleTypeId.Scp049)
                    {
                        scphpinfo = scphpinfo + "\n<color=#FFA500>SCP049</color>[<color=#FFFF00>" + player.Health + "/" + player.MaxHealth + "</color>][AHP:<color=#FF0000>" + player.HumeShield + "</color>][区域:<color=#FF0000>" + APIs.NYAPI.区域[player.Zone] + "</color>]";
                    }
                    if (player.Role == RoleTypeId.Scp096)
                    {
                        scphpinfo = scphpinfo + "\n<color=#FFA500><size=16>SCP096</color>[<color=#FFFF00>" + player.Health + "/" + player.MaxHealth + "</color>][AHP:<color=#FF0000>" + player.HumeShield + "</color>][区域:<color=#FF0000>" + APIs.NYAPI.区域[player.Zone] + "</color>]";
                    }
                    if (player.Role == RoleTypeId.Scp079)
                    {
                        if (player.Role is Scp079Role scp079Role)
                        {
                            scphpinfo = scphpinfo + "\n<color=#FFA500>SCP079</color><color=#FFFF00>[Online]电量:" + scp079Role.Energy + "</color>";
                        }
                    }
                    if (player.Role == RoleTypeId.Scp0492)
                    {
                        scp492num++;
                    }
                }
                catch
                {

                }

            }
            scphpinfo += ("\n<color=#FFA500>小僵尸数量</color>[" + scp492num + "]");
            foreach (var player1 in Player.List)
            {
                if (player1.Role.Team == Team.SCPs)
                {
                    foreach (var awa2 in awas)
                    {
                        if (player1.Id == awa2.Playeridawa)
                        {
                            string temp = "";

                            foreach (var message in awa2.Message)
                            {
                                if (message.Contains("SCP血量信息"))
                                {
                                    temp = message;
                                }
                            }
                            awa2.Message.Remove(temp);
                            StringBuilder str = new();
                            str.Append("\n<size=0><color=#00BFFF>[SCP血量信息]</color></size>\n<align=right><size=15>");
                            str.Append(scphpinfo);
                            str.Append("</size>");
                            awa2.Message.Add(str.ToString());
                            str.Clear();
                        }
                    }
                    continue;
                }
                foreach (Awa awa2 in awas)
                {
                    if (player1.Id == awa2.Playeridawa)
                    {
                        string temp = "";

                        foreach (string message in awa2.Message)
                        {
                            if (message.Contains("SCP血量信息"))
                            {
                                temp = message;
                            }
                        }
                        if (temp != "")
                        {
                            awa2.Message.Remove(temp);
                        }
                    }

                }
            }
            scphpinfo = "";
        }
        private static IEnumerator<float> NYServerHint()
        {
            yield return Timing.WaitForSeconds(5f);
            int awa = 0;
            while (true)
            {
                yield return Timing.WaitForSeconds(1f);
                awa++;
                if (awa >= 20)
                {
                    awa = 0;
                    try
                    {
                        GetSCPHP();
                    }
                    catch
                    {

                    }
                }
                for (int i = 0; i < awas.Count(); i++)
                {
                    try
                    {
                        if ((awas[i].Message.Count >= 1 && awas[i].Wait == false) || !awas[i].Playerawa.IsAlive)
                        {
                            StringBuilder str = new();
                            str.Append("<size=0>我是一般</size>");
                            foreach (var mm in awas[i].Message)
                            {
                                if (mm.Contains("SCP血量信息"))
                                {
                                    str.Insert(0, "\n\n\n\n\n\n\n\n");
                                    str.Insert(0, mm);
                                }
                                if (mm.Contains("聊天的Timing"))
                                {
                                    str.Insert(0, mm);
                                }
                                if (mm.Contains("玩家角色介绍"))
                                {
                                    str.Append(mm);
                                }
                                if (mm.Contains("临时消息"))
                                {
                                    str.Append(mm);
                                }

                            }
                            if (awas[i].Playerawa.IsConnected && awas[i].Playerawa.IsVerified)
                            {
                                if (awas[i].Playerawa.IsAlive)
                                {
                                    awas[i].Playerawa.ReferenceHub.hints.Show(new TextHint(str.ToString(), new HintParameter[1]
                                    {
                                        new StringHintParameter(str.ToString())
                                    }, durationScalar: 2));
                                }
                                

                            }
                            str.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Info(ex.Message);
                        Log.Info(ex.GetBaseException());
                    }
                }
            }
        }
        public static IEnumerator<float> ChatTiming(Player sendplayer, string chattxt)
        {
            yield return Timing.WaitForSeconds(1f);
            if (chattxt.Length <= 30)
            {
                if (chatList.Count <= 6)
                {
                    chatList.Add("<pos=35%>[" + sendplayer.Role.Type + "]" + sendplayer.Nickname + ":" + chattxt);
                }
                else
                {
                    chatList.RemoveAt(0);
                    chatList.Add("<pos=35%>[" + sendplayer.Role.Type + "]" + sendplayer.Nickname + ":" + chattxt);
                }
                List<string> list = new();
                for (int i = 0; i < chatList.Count; i++)
                {
                    string color = i switch
                    {
                        0 => "<color=#FFFF00>",
                        1 => "<color=#FFFF15>",
                        2 => "<color=#FFFF30>",
                        3 => "<color=#FFFF45>",
                        4 => "<color=#FFFF60>",
                        5 => "<color=#FFFF75>",
                        6 => "<color=#FFFF90>",
                        _ => "<color=#FFFF99>",
                    };
                    list.Add(color + chatList[i] + "</color>");
                }
                AddChatHint("<size=16><align=right>" + "<pos=35%>\n" + string.Join("\n", list) + "</size>");
                list.Clear();
            }
        }
        public static void AddTempHintToAll(string thing, int time)
        {
            foreach (var tmpplayer in Player.List)
            {
                AddTempHint(tmpplayer, thing, time);
            }
        }
        public static void AddTempHint(Player player, string thing, int time)
        {
            if (player.IsVerified)
            {
                foreach (Awa awa2 in awas)
                {
                    if (awa2.Playeridawa == player.Id)
                    {
                        awa2.Message.Add("\n<size=0>临时消息</size>\n" + thing);
                    }
                }
                Timing.CallDelayed(time, () => {
                    foreach (Awa awa2 in awas)
                    {
                        if (awa2.Playeridawa == player.Id)
                        {
                            string temp = "";
                            foreach (string message in awa2.Message)
                            {
                                if (message.Contains(thing))
                                {
                                    temp = message;
                                }
                            }
                            if (temp != "")
                            {
                                awa2.Message.Remove(temp);
                            }
                        }
                    }
                });
            }
        }
        public static void AddChatHint(string thing)
        {
            showchat = true;
            foreach (Awa awa2 in awas)
            {
                string temp = "";
                foreach (string message in awa2.Message)
                {
                    if (message.Contains("聊天的Timing"))
                    {
                        temp = message;
                    }
                }
                if (temp != "")
                {
                    awa2.Message.Remove(temp);
                }
                awa2.Message.Add("<size=0>聊天的Timing</size>\n" + thing);
            }
            Timing.CallDelayed(7f, () => {
                showchat = false;
                foreach (Awa awa2 in awas)
                {
                    string temp = "";
                    foreach (string message in awa2.Message)
                    {
                        if (message.Contains("聊天的Timing"))
                        {
                            temp = message;
                        }
                    }
                    if (temp != "")
                    {
                        awa2.Message.Remove(temp);
                    }
                }
            });
        }
        public static void OnVer(VerifiedEventArgs ev)
        {
            Awa tempawa = new()
            {
                Playerawa = ev.Player,
                Playeridawa = ev.Player.Id
            };
            awas.Add(tempawa);
        }
        private static void OnWaitingForPlayer()
        {
            Coroutines.Add(Timing.RunCoroutine(NYServerHint()));
        }

        private static void Reset()
        {
            showchat = false;
            chatList.Clear();
            foreach (Awa awa2 in awas)
            {
                awa2.Playerawa = null;
                awa2.Playeridawa = 0;
                awa2.Message.Clear();
            }
            awas.Clear();
            foreach (CoroutineHandle coroutineHandle in Coroutines)
            {
                Timing.KillCoroutines(coroutineHandle);
            }
            Coroutines.Clear();
        }
        public static void OnRoundEnd(RoundEndedEventArgs ev)
        {
            Reset();
        }
        public static void Register()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers += OnWaitingForPlayer;
            Exiled.Events.Handlers.Server.RoundEnded += OnRoundEnd;
            Exiled.Events.Handlers.Player.Verified += OnVer;
        }
        public static void Unregister()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers -= OnWaitingForPlayer;
            Exiled.Events.Handlers.Server.RoundEnded -= OnRoundEnd;
            Exiled.Events.Handlers.Player.Verified -= OnVer;
            Reset();
        }
    }
}
