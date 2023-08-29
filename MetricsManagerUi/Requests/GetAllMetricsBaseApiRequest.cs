using System;

namespace MetricsManagerUi.Requests
{
    public class GetAllMetricsBaseApiRequest
    {
        public string AgentAddress { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
