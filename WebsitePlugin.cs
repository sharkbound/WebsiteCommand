using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Core.Logging;
using Rocket.Core;

namespace WebsiteCommand
{
    public class WebsitePlugin : RocketPlugin<WebsiteConfig>
    {
        public static WebsitePlugin Instance;

        protected override void Load()
        {
            Instance = this;
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
            foreach (var command in Configuration.Instance.WebsiteCommands)
            {
                R.Commands.DeregisterFromAssembly(this.Assembly);
            }
        }
    }
}
