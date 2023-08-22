using MetricsAgent.DTO;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class AllCpuMetricsResponse
    {
        public IList<CpuMetricDto> Metrics { get; set; }
    }
}
