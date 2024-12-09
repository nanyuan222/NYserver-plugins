//©版权归小鲨鱼｜一只小杂鱼｜所有
using Exiled.API.Interfaces;
using PlayerRoles;
using System.Collections.Generic;
using System.ComponentModel;

namespace NYPlugin
{
    public class Config : IConfig
    {
        [Description("是否开启插件")]
        public bool IsEnabled { get; set; } = true;
        [Description("是否开启Debug")]
        public bool Debug { get; set; } = true;
        
        [Description("入服欢迎")]
        public string Message { get; set; } = "<color=red>久等了%name%同志！请阅读规则</color>";

        [Description("欢迎提示时间")]
        public ushort MessageDuration { get; set; } = 6;
        [Description("可乐免伤")]
        public bool Scp207 { get; set; } = true;
        [Description("是否开启清洁程序")]
        public bool Clean { get; set; } = true;
        [Description("清理间隔时间(秒)")]
        public ushort WaitClean { get; set; } = 300;
        [Description("清理完成的公告,{0}代表清理的物品数量{1}为玩家尸体数量")]
        public string CleanedBroadcast { get; set; } = "<b>[<color=#EEEE00>扫地机器人</color>]吃干净啦! 清理了{0}个物品和{1}个杂鱼~</b>";
        [Description("开局给物品")]
        public Dictionary<RoleTypeId, List<ItemType>> Inventory { get; set; } = new Dictionary<RoleTypeId, List<ItemType>>()
        {
            //dd开局给清洁工卡
            { RoleTypeId.ClassD, new List<ItemType> { ItemType.KeycardJanitor } }
        };
        public string Team { get; set; } = "<align=left><size=28><color=#70EE9C>你的队友</color></size> \n<size=25><color=yellow>{0}</color></size></align>";
    }
}
