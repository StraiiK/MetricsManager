using System;

namespace MetricsAgent.DTO
{
    public class MetricBaseDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
