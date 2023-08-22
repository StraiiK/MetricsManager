using MetricsAgent.DTO;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class AllDotNetMetricsResponse
    {
        public IList<DotNetMetricDto> Metrics { get; set; }
    }
}
