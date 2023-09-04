using MetricsManager.DTO;
using System;

namespace MetricsManager.DAL.Interfaces
{
    public interface IRomMetricsRepository : IRepository<RomMetricDto>, IDisposable
    {
    }
}
