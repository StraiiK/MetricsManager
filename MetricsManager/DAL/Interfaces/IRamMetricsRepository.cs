using MetricsManager.DTO;
using System;

namespace MetricsManager.DAL.Interfaces
{
    public interface IRamMetricsRepository : IRepository<RamMetricDto>, IDisposable
    {
    }
}
