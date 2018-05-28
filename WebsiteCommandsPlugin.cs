using Rocket.API.Commands;
using Rocket.API.DependencyInjection;
using Rocket.API.Eventing;
using Rocket.Core.Player.Events;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;

namespace WebsiteCommand
{
    public class WebsiteCommandsPlugin : Plugin<WebsiteConfig>, IEventListener<PlayerConnectedEvent>
    {
        public WebsiteCommandsPlugin(IDependencyContainer container) : base("WebsiteCommands", container)
        {
        }

        protected override void OnLoad(bool isFromReload)
        {
            var provider = (WebsiteCommandProvider) Container.Resolve<ICommandProvider>("website_commands");
            if(isFromReload)
                provider.Rebuild();

            EventManager.AddEventListener(this, this);
            Logger.Log("WebsiteCommand has loaded!");
        }

        protected override void OnUnload()
        {
            Logger.Log("WebsiteCommand has Unloaded!");
        }

        public static void OpenUrl(UnturnedPlayer player, string url, string description)
        {
            player.NativePlayer.sendBrowserRequest(description, url);
        }

        public void HandleEvent(IEventEmitter emitter, PlayerConnectedEvent @event)
        {
            if (!ConfigurationInstance.OpenUrlOnJoin || !(@event.Player is UnturnedPlayer player))
                return;

            OpenUrl(player, ConfigurationInstance.JoinUrl, ConfigurationInstance.JoinDesc);
        }
    }
}
