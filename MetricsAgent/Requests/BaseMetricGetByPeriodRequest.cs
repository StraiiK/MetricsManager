using System;

namespace MetricsAgent.Requests
{
    public class BaseMetricGetByPeriodRequest
    {
        public DateTimeOffset fromTime { get; set; }
        public DateTimeOffset toTime { get; set; }
    }
}
