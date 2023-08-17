using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class AllBaseMetricsResponse
    {
        public List<BaseMetricDto> Metrics { get; set; }
    }
}
