using MetricsManager.DTO;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class AllCpuMetricsApiResponse
    {
        public IList<CpuMetricDto> Metrics { get; set; }
    }
}
