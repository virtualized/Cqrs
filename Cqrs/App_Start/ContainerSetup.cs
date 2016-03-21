using Cqrs.Application.Query.Handlers;
using Cqrs.Infrastructure.Dapper;
using SimpleInjector;
using System.Configuration;
using System.Linq;
using System.Reflection;

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
            container.Register<IDapperConnectionFactory, DapperConnectionFactory>();
            container.Register(typeof(IQueryHandler<,>), new[] { typeof(IQueryHandler<,>).Assembly } );
        }

        private static void RegisterConfigurationComponents(Container container)
        {
            container.Register<ConnectionStringProvider>(Lifestyle.Singleton);
            container.RegisterInitializer<ConnectionStringProvider>(c =>
            {
                c.ConnectionString = ConfigurationManager.ConnectionStrings["CqrsContext"].ConnectionString;
            });
        }
    }
}