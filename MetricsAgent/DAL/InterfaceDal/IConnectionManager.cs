using System.Data.SQLite;

namespace MetricsAgent.DAL.InterfaceDal
{
    public interface IConnectionManager
    {
        SQLiteConnection CreateOpenedConnection();
    }
}
