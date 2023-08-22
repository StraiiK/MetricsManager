using MetricsAgent.DTO;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class AllRamMetricsResponse
    {
        public IList<RamMetricDto> Metrics { get; set; }
    }
}
