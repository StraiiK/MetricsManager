using MetricsManagerUi.DTO;
using System.Collections.Generic;

namespace MetricsManagerUi.Responses
{
    public class AllCpuMetricsApiResponse
    {
        public IList<CpuMetricDto> Metrics { get; set; }
    }
}
