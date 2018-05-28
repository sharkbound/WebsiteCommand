using Rocket.API.Commands;
using Rocket.API.DependencyInjection;

namespace WebsiteCommand.Properties
{
    public class DependencyRegistrator : IDependencyRegistrator
    {
        public void Register(IDependencyContainer container, IDependencyResolver resolver)
        {
            container.RegisterSingletonType<ICommandProvider, WebsiteCommandProvider>("website_commands");
        }
    }
}