using System;

namespace MetricsManager.DAL.Models
{
    public class BaseMetricDal
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int Value { get; set; }
        public long Time { get; set; }
    }
}
