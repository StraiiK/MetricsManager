using System;

namespace MetricsManager.DTO
{
    public class MetricBaseDto
    {
        public int Id { get; set; }
        public int? AgentId { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }        
    }
}
