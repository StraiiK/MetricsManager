using MetricsAgent.DTO;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class AllRomMetricsResponse
    {
        public IList<RomMetricDto> Metrics { get; set; }
    }
}
