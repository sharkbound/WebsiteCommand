using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Core.Logging;
using Rocket.Core;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Threading;

namespace WebsiteCommand
{
    public class WebsitePlugin : RocketPlugin<WebsiteConfig>
    {
        public static WebsitePlugin Instance;

        protected override void Load()
        {
            Instance = this;

            U.Events.OnPlayerConnected += Events_OnPlayerConnected;

            Logger.Log("WebsiteCommand has loaded!");
            if (Configuration != null && Configuration.Instance.WebsiteCommands.Count != 0)
            {
                foreach (var command in Configuration.Instance.WebsiteCommands)
                {
                    RocketWebsiteCommand cmd = new RocketWebsiteCommand(command.CommandName, command.Desc, command.Url, command.Help);
                    R.Commands.Register(cmd);
                }
            }
        }

        protected override void Unload()
        {
            Logger.Log("WebsiteCommand has Unloaded!");

            U.Events.OnPlayerConnected -= Events_OnPlayerConnected;

            foreach (var command in Configuration.Instance.WebsiteCommands)
            {
                R.Commands.DeregisterFromAssembly(this.Assembly);
            }
        }

        void Events_OnPlayerConnected(Rocket.Unturned.Player.UnturnedPlayer player)
        {
            if (Configuration.Instance.OpenUrlOnJoin)
            {
                new Thread(() =>
                {
                    Thread.Sleep(1500);
                    OpenUrl(player, Configuration.Instance.JoinUrl, Configuration.Instance.JoinDesc);
                }).Start();
            }
        }

        public static void OpenUrl(UnturnedPlayer player, string url, string desc)
        {
            player.Player.channel.send("askBrowserRequest", player.CSteamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, desc, url);
        }
    }
}
