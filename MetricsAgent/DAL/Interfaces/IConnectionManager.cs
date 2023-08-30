using System.Data.SQLite;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IConnectionManager
    {
        string ConnectionString { get;}
        SQLiteConnection CreateOpenedConnection();
    }
}
