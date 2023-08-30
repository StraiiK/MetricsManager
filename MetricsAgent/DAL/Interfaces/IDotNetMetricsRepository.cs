using MetricsAgent.DAL.Models;
using MetricsAgent.DTO;
using System;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetricDto>, IDisposable
    {
    }
}
