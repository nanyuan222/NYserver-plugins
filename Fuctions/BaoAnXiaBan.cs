using Exiled.API.Features;
using MEC;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NYPlugin.Fuctions
{
    public class BaoAnXiaBan
    {
        public static void RoundStart()
        {
            Timing.RunCoroutine(BaoAnXiaBanTiming());
        }
        private static IEnumerator<float> BaoAnXiaBanTiming()
        {
            yield return Timing.WaitForSeconds(1);
            while (Round.IsStarted)
            {
                yield return Timing.WaitForSeconds(1);
                foreach (var variablPlayer in Player.List)
                {
                    if (Vector3.Distance(variablPlayer.Position, Escape.WorldPos) <= 10)
                    {
                        if (variablPlayer.Role == RoleTypeId.FacilityGuard)
                        {

                            variablPlayer.Role.Set(RoleTypeId.NtfSergeant);
                        }
                    }
                }
            }
        }
        public static void Register()
        {
            Exiled.Events.Handlers.Server.RoundStarted += RoundStart;
            Log.Info("保安下班加载完毕");
        }
        public static void UnRegister()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= RoundStart;
        }
    }
}
