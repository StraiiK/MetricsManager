using MetricsAgent.DAL.InterfaceDal;
using System;
using System.Data.SQLite;
using System.IO;

namespace MetricsAgent.DAL
{
    public class ConnectionManager: IConnectionManager
    {
        private const string _connectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        public SQLiteConnection CreateOpenedConnection()
        {
            var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
