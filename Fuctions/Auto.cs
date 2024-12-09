using Exiled.API.Features.Pickups;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Extensions;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace NYPlugin.Fuctions
{
    public class Auto
    {
        public static CoroutineHandle 清洁程序;
        public static void Start()
        {
            if (Plugin.Instance.Config.Clean == true)
            {
                清洁程序 = Timing.RunCoroutine(CleanSys());
                Map.Broadcast(5, "[SYSTEM]已开启清理模块");
            }
        }

        public static void End(RoundEndedEventArgs e)
        {
            if (Plugin.Instance.Config.Clean == true)
            {
                Timing.KillCoroutines(清洁程序);
            }
        }

        public static IEnumerator<float> CleanSys()
        {

            yield return Timing.WaitForSeconds(Plugin.Instance.Config.WaitClean);
            for (; ; )
            {
                int ragdollnum = 0;
                int itemnum = 0;

                foreach (Ragdoll ragdoll in Ragdoll.List.ToHashSet())
                {
                    ragdoll.Destroy();
                    int num = ragdollnum;
                    ragdollnum = num + 1;
                }

                foreach (Pickup item in Pickup.List.ToHashSet())
                {
                    bool flag = !item.Type.IsScp() && !item.Type.IsKeycard() && !item.Type.IsMedical() && !item.Type.IsThrowable() && item.Type != ItemType.MicroHID && !item.Type.IsWeapon() && !item.Type.IsAmmo() && !item.Type.IsWeapon(true);
                    if (flag)
                    {
                        item.Destroy();
                        int num = itemnum;
                        itemnum = num + 1;
                    }
                }

                Timing.CallDelayed(5f, delegate ()
                {
                    Map.Broadcast(10, string.Format(Plugin.Instance.Config.CleanedBroadcast, itemnum, ragdollnum), 0, true);
                });

                yield return Timing.WaitForSeconds(Plugin.Instance.Config.WaitClean);
            }
        }
        public void OnSpawned(SpawnedEventArgs e)
        {
            var role = e.Player.Role;
            if (Plugin.Instance.Config.Inventory.TryGetValue(role, out var inventory) && role.SpawnFlags.HasFlag(RoleSpawnFlags.AssignInventory))
            {
                e.Player.ClearItems();
                inventory.ForEach(item => e.Player.AddItem(item));
            }
        }
        private void OnPlayerVerified(VerifiedEventArgs ev)
        {
            ev.Player.Broadcast(Plugin.Instance.Config.MessageDuration, Plugin.Instance.Config.Message);
        }
        public void OnEnabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted += Start;
            Exiled.Events.Handlers.Server.RoundEnded += End;
            Exiled.Events.Handlers.Player.Verified += OnPlayerVerified;
        }

        public void OnDisabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= Start;
            Exiled.Events.Handlers.Server.RoundEnded += End;
            Exiled.Events.Handlers.Player.Verified -= OnPlayerVerified;
        }
    }
}
