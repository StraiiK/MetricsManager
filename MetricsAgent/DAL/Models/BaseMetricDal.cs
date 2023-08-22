using System;

namespace MetricsAgent.DAL.Models
{
    public class BaseMetricDal
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public long Time { get; set; }
    }
}
