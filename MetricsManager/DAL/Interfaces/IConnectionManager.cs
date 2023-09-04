using System.Data.SQLite;

namespace MetricsManager.DAL.Interfaces
{
    public interface IConnectionManager
    {
        string ConnectionString { get; }
        SQLiteConnection CreateOpenedConnection();        
    }
}
