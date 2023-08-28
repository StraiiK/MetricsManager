using System;
using System.Collections.Generic;

namespace MetricsManager.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetByPeriodFromAgent(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime);
        IList<T> GetByPeriodFromAllCluster(DateTimeOffset fromTime, DateTimeOffset toTime);
        IList<T> GetAll();
        void Create(T item);
        public DateTimeOffset GetLastOfTime(int agentId);
    }
}
