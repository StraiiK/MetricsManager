using System;
using System.Data.SQLite;
using System.IO;
using MetricsAgent.DAL.Interfaces;

namespace MetricsAgent.DAL
{
    public class ConnectionManager : IConnectionManager
    {
        public SQLiteConnection CreateOpenedConnection()
        {
            var projectFolder = Path.GetRelativePath(Environment.CurrentDirectory, AppDomain.CurrentDomain.BaseDirectory);
            string connectionString = $"Data Source={projectFolder}metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
