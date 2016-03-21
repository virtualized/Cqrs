using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Cqrs.Connections.Dapper
{
    public class DapperConnectionFactory : IConnectionFactory
    {
        private readonly ConnectionStringProvider provider;

        public DapperConnectionFactory(ConnectionStringProvider provider)
        {
            this.provider = provider;
        }

        public IConnection CreateConnection()
        {
            // TODO: Factorize!
            return new DapperConnection(provider);
        }
    }

    public class DapperConnection : IConnection
    {
        private bool disposed;
        private readonly IDbConnection connection;

        public DapperConnection(IConnectionStringProvider connectionStringProvider)
        {
            connection = new SqlConnection(connectionStringProvider.ConnectionString);
        }

        public void Open()
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }

        public void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TResult> Query<TResult>(string sql, object param = null)
        {
            return connection.Query<TResult>(sql, param, null, true, default(int?), CommandType.Text);
        }

        public int Execute(string sql, object param = null)
        {
            return connection.Execute(sql, param, null, default(int?), CommandType.Text);
        }

        public Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object param = null)
        {
            return connection.QueryAsync<TResult>(sql, param);
        }
    }
}