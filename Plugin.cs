using Exiled.API.Features;
using NYPlugin.Fuctions;
using System.Collections.Generic;

//©版权归小鲨鱼｜一只小杂鱼｜所有
namespace NYPlugin
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "小鲨鱼";
        public override string Name { get; } = "NYPlugin";
        private static Plugin instance;
        public static Plugin Instance => instance;
        IEnumerable<Config> config;       
        private void RegisterEvents()
        {
            BaoAnXiaBan.Register();
            Fuctions.Servers.Hint.Register();
            instance = this;
        }
        private void UnregisterEvents()
        {
            BaoAnXiaBan.UnRegister();
            Fuctions.Servers.Hint.Unregister();
            instance = null;
        }
        
    }
}
