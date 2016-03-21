using Cqrs.Connections;
using Cqrs.Connections.Dapper;
using System;
using System.Configuration;

namespace Cqrs.Tools.ExampleInitializer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating database...");
            var dapperConnectionFactory = GetFactory("InstanceContext");
            using (var dapperConnection = dapperConnectionFactory.CreateConnection())
            {
                dapperConnection.Open();
                dapperConnection.Execute("IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'CqrsContext') CREATE DATABASE CqrsContext");
            }

            dapperConnectionFactory = GetFactory("CqrsContext");
            using (var dapperConnection = dapperConnectionFactory.CreateConnection())
            {
                dapperConnection.Open();
                dapperConnection.Execute("IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'Users') CREATE TABLE Users (Name nvarchar(128) NULL)");

                dapperConnection.Execute("IF NOT EXISTS(SELECT * FROM Users WHERE Name = 'James') INSERT INTO Users (Name) VALUES ('James')");
            }
            Console.WriteLine("Database created!");
            Console.WriteLine("Press any key to continue...");

            Console.ReadLine();
        }

        private static DapperConnectionFactory GetFactory(string connectionStringName)
        {
            return new DapperConnectionFactory(new ConnectionStringProvider { ConnectionString = GetConnectionString(connectionStringName) });
        }

        private static string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}