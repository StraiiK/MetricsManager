using System;

namespace MetricsAgent.Requests
{
    public class BaseMetricCreateRequest
    {
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
