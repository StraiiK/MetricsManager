using MetricsAgent.DTO;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class AllRomMetricsResponse
    {
        public List<RomMetricDto> Metrics { get; set; }
    }
}
