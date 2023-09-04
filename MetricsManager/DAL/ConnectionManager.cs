using System;
using System.Data.SQLite;
using System.IO;
using MetricsManager.DAL.Interfaces;

namespace MetricsManager.DAL
{
    public class ConnectionManager : IConnectionManager
    {
        private string ProjectFolder => Path.GetRelativePath(Environment.CurrentDirectory, AppDomain.CurrentDomain.BaseDirectory);
        private string PathToDb => Path.Combine(ProjectFolder, "metrics.db");
        public string ConnectionString => $"Data Source={PathToDb};";
        
        public SQLiteConnection CreateOpenedConnection()
        {
            var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
