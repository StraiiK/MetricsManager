using MetricsManager.DTO;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class AllRomMetricsApiResponse
    {
        public IList<RomMetricDto> Metrics { get; set; }
    }
}
