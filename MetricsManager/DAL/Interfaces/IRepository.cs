using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task <IList<T>> GetByPeriodFromAgentAsync(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime, CancellationToken cancellationToken = default);
        Task<IList<T>> GetByPeriodFromAllClusterAsync(DateTimeOffset fromTime, DateTimeOffset toTime, CancellationToken cancellationToken = default);
        Task<IList<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task CreateAsync(T item, CancellationToken cancellationToken = default);
        public Task <DateTimeOffset> GetLastOfTimeAsync(int agentId, CancellationToken cancellationToken = default);
    }
}
