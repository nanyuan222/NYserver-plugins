using Exiled.Events.EventArgs.Player;
using Exiled.Events.Features;

namespace NYPlugin.Fuctions
{
    public class Scp207
    {
        private void OnHurting(HurtingEventArgs args)
        {
            if (Plugin.Instance.Config.Scp207 == true && args.DamageHandler.Type == Exiled.API.Enums.DamageType.Scp207)
            {
                args.IsAllowed = false;
            }
        }
        public void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting += new CustomEventHandler<HurtingEventArgs>(OnHurting);
        }

        public void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting -= new CustomEventHandler<HurtingEventArgs>(OnHurting);
        }
    }
}
