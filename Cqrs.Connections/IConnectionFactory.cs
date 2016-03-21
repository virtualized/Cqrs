using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cqrs.Connections
{
    public interface IConnectionFactory
    {
        IConnection CreateConnection();
    }

    public interface IConnection : IDisposable
    {
        void Open();
        int Execute(string sql, object param = null);
        IEnumerable<TResult> Query<TResult>(string sql, object param = null);
        Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object param = null);
    }

    public interface IConnectionStringProvider
    {
        string ConnectionString { get; }
    }

    public class ConnectionStringProvider : IConnectionStringProvider
    {
        public string ConnectionString { get; set; }
    }
}