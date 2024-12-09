using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
//©版权归小鲨鱼｜一只小杂鱼｜所有

namespace NYPlugin.APIs
{
    public class NYAPI
    {
        public static int radomseed;
        public static ItemType GetRamdomItemType(bool mustscp)
        {
            var itemTypes = new List<ItemType>();
            for (var i = 0; i <= 53; i++)
            {
                if (!mustscp)
                {
                    var item = (ItemType)i;
                    if (!item.IsAmmo() && item != ItemType.MicroHID && item != ItemType.Jailbird && item != ItemType.ParticleDisruptor)
                    {
                        itemTypes.Add(item);
                    }
                }
                else
                {
                    var item = (ItemType)i;
                    if (!item.IsScp())
                    {
                        itemTypes.Add(item);
                    }
                }
                 
            }
            return itemTypes[new Random(Environment.TickCount + (radomseed++)).Next(0, itemTypes.Count() - 1)];
        }
        public static List<T> RandomSort<T>(List<T> list)
        {
            var random = new System.Random();
            var newList = new List<T>();
            foreach (var item in list)
            {
                newList.Insert(random.Next(newList.Count), item);
            }
            return newList;
        }
        public static Player GetRamdomPlayer(Team team)
        {
            var players = Player.Get(team).Where(player => player.Role.Type != RoleTypeId.Scp079 ).ToList();
            return players.Any() ? players[new System.Random(Environment.TickCount + (radomseed++)).Next(0, players.Count() - 1)] : null;
        }
        public static Dictionary<ZoneType, string> 区域 = new Dictionary<ZoneType, string> {

            {ZoneType.Surface,"地表" },
            {ZoneType.Other,"其他" },
            {ZoneType.HeavyContainment,"重收容" },
            {ZoneType.Entrance,"办公区" },
            {ZoneType.LightContainment,"轻收容" },
            {ZoneType.Unspecified,"未知" },
        };
    }
}
