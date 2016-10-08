using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;
using System.Xml.Serialization;

namespace WebsiteCommand
{
    public class WebsiteConfig : IRocketPluginConfiguration
    {
        [XmlArrayItem(ElementName = "WebsiteCommand")]
        public List<WebsiteCommand> WebsiteCommands;

        public void LoadDefaults()
        {
            WebsiteCommands = new List<WebsiteCommand> { new WebsiteCommand{CommandName = "default", Desc = "default", Help = "default", Url = "default"}};
        }
    }

    public class WebsiteCommand
    {
        public string Url;
        public string Desc;
        public string CommandName;
        public string Help;
    }
}
