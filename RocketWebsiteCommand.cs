using System;
using Rocket.API.Commands;
using Rocket.Core.DependencyInjection;
using Rocket.Unturned.Player;

namespace WebsiteCommand
{
    [DontAutoRegister]
    public class RocketWebsiteCommand : ICommand
    {
        public RocketWebsiteCommand(string name, string  description, string url, string permission)
        {
            Summary = description;
            Url = url;
            Name = name;
            Permission = permission;
        }

        public string Name { get; }
        public string[] Aliases => null;
        public string Summary { get; }
        public string Description => null;
        public string Permission { get; }
        public string Syntax => "";
        public IChildCommand[] ChildCommands => null;

        public string Url { get; }

        public bool SupportsUser(Type user)
        {
            return typeof(UnturnedPlayer).IsAssignableFrom(user);
        }

        public void Execute(ICommandContext context)
        {
            UnturnedPlayer uCaller = ((UnturnedUser)context.User).Player;
            WebsiteCommandsPlugin.OpenUrl(uCaller, Url, Summary);
        }
    }
}
