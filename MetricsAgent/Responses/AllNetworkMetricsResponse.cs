using MetricsAgent.DTO;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class AllNetworkMetricsResponse
    {
        public IList<NetworkMetricDto> Metrics { get; set; }
    }
}
