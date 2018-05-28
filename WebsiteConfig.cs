using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;
using System.Xml.Serialization;
using Rocket.Core.Configuration;

namespace WebsiteCommand
{
    public class WebsiteConfig
    {
        public bool OpenUrlOnJoin { get; set; } = false;
        public string JoinUrl { get; set; } = "www.google.com";
        public string JoinDesc { get; set; } = "website:";

        [ConfigArray]
        public WebsiteCommand[] WebsiteCommands { get; set; } =
        {
            new WebsiteCommand
            {
                CommandName = "discord",
                Description = "Join our discord!",
                Url = "https://discord.gg",
                Permission = "url.discord"
            }
        };
    }

    public class WebsiteCommand
    {
        public string Url { get; set; }
        public string Description { get; set; }
        public string CommandName { get; set; }
        public string Permission { get; set; }
    }
}
