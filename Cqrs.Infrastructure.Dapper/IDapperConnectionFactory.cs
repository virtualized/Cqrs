using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Cqrs.Infrastructure.Dapper
{
    public interface IDapperConnectionFactory
    {
        IDapperConnection CreateConnection();
    }

    public interface IDapperConnection : IDisposable
    {
        void Open();
        int Execute(string sql, object param = null, CommandType? commandType = default(CommandType?));
        IEnumerable<T> Query<T>(string sql, object param = null, CommandType? commandType = default(CommandType?));
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType? commandType = default(CommandType?));
    }

    public class DapperConnectionFactory : IDapperConnectionFactory
    {
        private readonly ConnectionStringProvider connectionStringProvider;

        public DapperConnectionFactory(ConnectionStringProvider connectionStringProvider)
        {
            this.connectionStringProvider = connectionStringProvider;
        }

        public IDapperConnection CreateConnection()
        {
            return new DapperConnection(connectionStringProvider.ConnectionString);
        }
    }

    public sealed class DapperConnection : IDapperConnection
    {
        private bool disposed;
        private readonly IDbConnection connection;

        public DapperConnection(string connectionString)
        {
            this.connection = new SqlConnection(connectionString);
        }

        public void Open()
        {
            if (this.connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public int Execute(string sql, object param = null, CommandType? commandType = 0)
        {
            return connection.Execute(sql, param, null, default(int?), commandType);
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, CommandType? commandType = 0)
        {
            return connection.Query<T>(sql, param, null, true, default(int?), commandType);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType? commandType = default(CommandType?))
        {
            return connection.QueryAsync<T>(sql, param, null, null, commandType);
        }

        public void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {

                if (this.connection.State != ConnectionState.Closed)
                {
                    this.connection.Dispose();
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public class ConnectionStringProvider
    {
        public string ConnectionString { get; set; }
    }
}