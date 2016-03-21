using Cqrs.Application.Query.Handlers;
using Cqrs.Infrastructure.Dapper;
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
            container.Register<IDapperConnectionFactory, DapperConnectionFactory>();
            container.Register(typeof(IQueryHandler<,>), typeof(GetAllCustomersQueryHandler));
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