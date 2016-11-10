using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;

namespace WebsiteCommand
{
    public class RocketWebsiteCommand : IRocketCommand
    {
        private string name;
        private string help;
        private string url;
        private string desc;


        public RocketWebsiteCommand(string name, string desc, string url, string cmdHelp)
        {
            this.name = name;
            this.help = cmdHelp;
            this.url = url;
            this.desc = desc;
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public AllowedCaller AllowedCaller
        {
            get { return Rocket.API.AllowedCaller.Player; }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer uCaller = (UnturnedPlayer)caller;

            //uCaller.Player.channel.send("askBrowserRequest", uCaller.CSteamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, desc, url);
            WebsitePlugin.OpenUrl(uCaller, url, desc);
        }

        public string Help
        {
            get { return help; }
        }

        public string Name
        {
            get { return name; }
        }

        public List<string> Permissions
        {
            get { return new List<string>(); }
        }

        public string Syntax
        {
            get { return ""; }
        }
    }
}
