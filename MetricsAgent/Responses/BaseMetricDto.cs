using System;

namespace MetricsAgent.Responses
{
    public class BaseMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
