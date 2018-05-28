using System.Collections.Generic;
using System.Linq;
using Rocket.API;
using Rocket.API.Commands;
using Rocket.API.Plugins;

namespace WebsiteCommand
{
    public class WebsiteCommandProvider : ICommandProvider
    {
        private readonly IPluginManager _pluginManager;

        public WebsiteCommandProvider(IPluginManager pluginManager)
        {
            _pluginManager = pluginManager;
        }

        public string ServiceName => "WebsiteCommands";
        public ILifecycleObject GetOwner(ICommand command)
        {
            return _pluginManager.GetPlugin("WebsiteCommands");
        }

        public void Init()
        {
        }

        public void Rebuild()
        {
            _commands.Clear();
        }

        private readonly List<ICommand> _commands = new List<ICommand>();
        public IEnumerable<ICommand> Commands
        {
            get
            {
                var owner = (WebsiteCommandsPlugin) GetOwner(null);
                if(!owner.IsAlive)
                    return new List<ICommand>();

                foreach (var cmd in owner.ConfigurationInstance.WebsiteCommands)
                {
                    if(_commands.Any(c => c.Name.Equals(cmd.CommandName)))
                        continue;

                    _commands.Add(new RocketWebsiteCommand(cmd.CommandName, cmd.Description, cmd.Url, cmd.Permission));
                }

                return _commands;
            }
        }
    }
}