using CommandSystem;
using CustomPlayerEffects;
using Exiled.API.Features;
using MEC;
using Mono.Cecil;
using PlayerRoles;
using SCPSLAudioApi.AudioCore;
using System;
using System.Collections.Generic;
using YongAnFrame.Core;
using static PlayerList;
using static YongAnFrame.Core.MusicManager;
using Player = Exiled.API.Features.Player;
using Round = Exiled.API.Features.Round;
using Warhead = Exiled.API.Features.Warhead;

namespace NYPlugin.MiniGames
{
    public class Level
    {
        private AudioPlayerBase audioPlayerBase = null;
        [CommandHandler(typeof(RemoteAdminCommandHandler))]
        public class LevelCommand : ICommand
        {
            public string Command => "level !";

            public string[] Aliases => ["level"];

            public string Description => "level !";
            public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
            {
                if (Player.TryGet(sender, out Player player))
                {
                    Timing.RunCoroutine(Level());
                    response = "Ok";
                    return true;
                }
                response = "No";

                return false;
            }
            private static IEnumerator<float> Level()
            {
                yield return Timing.WaitForSeconds(1);
                while (Round.IsStarted)
                {
                    yield return Timing.WaitForSeconds(1);                    
                    foreach (var Player in Player.List)
                    {
                        if (Player.Role != RoleTypeId.ClassD)
                        {
                            Player.Role.Set(RoleTypeId.ClassD);
                            Player.EnableEffect<MovementBoost>();
                            Player.ChangeEffectIntensity<MovementBoost>(50);
                            //MusicManager.Instance.Play(musicFile, npcId, npcName, trackEvent, false, 80, false, source, distance);
                        }
                        Warhead.Start();
                        Warhead.IsLocked = true;
                        Timing.CallDelayed(85f, () =>
                        {
                            if (Player.Position.y < 980f)
                            {
                                Player.Kill("你咋这么慢呢ㄟ( ▔, ▔ )ㄏ");
                            }
                            Warhead.IsLocked = false;
                            Warhead.Stop();
                        });
                    }
                }
            }
            [CommandHandler(typeof(RemoteAdminCommandHandler))]
            public class LevelStop : ICommand
            {
                public string Command => "level ! stop";

                public string[] Aliases => new string[1] { "level stop" };

                public string Description => "level! stop";
                public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
                {
                    if (Player.TryGet(sender, out Player player))
                    {
                        Warhead.IsLocked = false;
                        Warhead.Stop();
                        if (player.Role == RoleTypeId.ClassD)
                        {
                            player.Role.Set(RoleTypeId.Overwatch);                          
                        }
                        response = "Ok";
                        return true;
                    }
                    response = "No";

                    return false;
                }
            }
        }
    }
}
