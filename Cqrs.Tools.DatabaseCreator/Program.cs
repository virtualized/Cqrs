using Cqrs.Infrastructure.Dapper;
using System;
using System.Configuration;

namespace Cqrs.Tools.DatabaseCreator
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
                dapperConnection.Execute("IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'Customers') CREATE TABLE Customers (Name nvarchar(128) NULL)");

                dapperConnection.Execute("IF NOT EXISTS(SELECT * FROM Customers WHERE Name = 'James') INSERT INTO Customers (Name) VALUES ('James')");
            }
            Console.WriteLine("Database created!");

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
