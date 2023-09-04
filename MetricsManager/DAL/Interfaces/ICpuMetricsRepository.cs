using MetricsManager.DTO;
using System;

namespace MetricsManager.DAL.Interfaces
{
    public interface ICpuMetricsRepository : IRepository<CpuMetricDto>, IDisposable
    {
    }
}
