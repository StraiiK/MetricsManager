using MetricsManager.DTO;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class AllNetworkMetricsApiResponse
    {
        public IList<NetworkMetricDto> Metrics { get; set; }
    }
}
