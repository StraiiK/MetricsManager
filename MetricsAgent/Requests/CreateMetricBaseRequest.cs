using System;

namespace MetricsAgent.Requests
{
    public class CreateMetricBaseRequest
    {
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
