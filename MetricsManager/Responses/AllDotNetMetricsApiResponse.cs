using MetricsManager.DTO;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class AllDotNetMetricsApiResponse
    {
        public IList<DotNetMetricDto> Metrics { get; set; }
    }
}
