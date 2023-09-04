using MetricsManager.DTO;
using System;

namespace MetricsManager.DAL.Interfaces
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetricDto>, IDisposable
    {
    }
}
