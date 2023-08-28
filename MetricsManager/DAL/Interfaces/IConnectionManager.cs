using System.Data.SQLite;

namespace MetricsManager.DAL.Interfaces
{
    public interface IConnectionManager
    {
        SQLiteConnection CreateOpenedConnection();
    }
}
