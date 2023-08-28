using MetricsManager.DTO;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class AllRamMetricsApiResponse
    {
        public IList<RamMetricDto> Metrics { get; set; }
    }
}
