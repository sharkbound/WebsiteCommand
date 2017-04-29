using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Core;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Threading;
using UnityEngine;

using Logger = Rocket.Core.Logging.Logger;

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

        void Events_OnPlayerConnected(UnturnedPlayer player)
        {
            if (player != null && Configuration.Instance.OpenUrlOnJoin)
            {
                StartCoroutine(StartDelayedUrlRequest(player));
            }
        }

        public static void OpenUrl(UnturnedPlayer player, string url, string desc)
        {
            player.Player.channel.send("askBrowserRequest", player.CSteamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, desc, url);
        }

        IEnumerator<WaitForSeconds> StartDelayedUrlRequest(UnturnedPlayer player)
        {
            yield return new WaitForSeconds(1.5f);
            OpenUrl(player, Configuration.Instance.JoinUrl, Configuration.Instance.JoinDesc);
        }
    }
}
