using Cqrs.Application.Queries.Handlers;
using Cqrs.Connections;
using Cqrs.Connections.Dapper;
using Cqrs.Querying;
using SimpleInjector;
using System.Configuration;

namespace Cqrs
{
    public class ContainerSetup
    {
        public static Container RegisterComponentsTo(Container container)
        {
            RegisterQueryComponents(container);
            RegisterConfigurationComponents(container);
            return container;
        }

        private static void RegisterQueryComponents(Container container)
        {
            container.Register(typeof(IConnectionFactory), typeof(DapperConnectionFactory));
            container.Register(typeof(IQueryHandler<,>), new[] { typeof(FindUsersBySearchTextQueryHandler).Assembly });
            container.Register(typeof(IAsyncQueryHandler<,>), new[] { typeof(FindUsersBySearchTextAsyncQueryHandler).Assembly });
        }

        private static void RegisterConfigurationComponents(Container container)
        {
            container.Register<IConnectionStringProvider, ConnectionStringProvider>(Lifestyle.Singleton);
            container.RegisterInitializer<ConnectionStringProvider>(c =>
            {
                c.ConnectionString = ConfigurationManager.ConnectionStrings["CqrsContext"].ConnectionString;
            });
        }
    }
}