using MetricsManager.DTO;
using System;

namespace MetricsManager.DAL.Interfaces
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetricDto>, IDisposable
    {
    }
}
