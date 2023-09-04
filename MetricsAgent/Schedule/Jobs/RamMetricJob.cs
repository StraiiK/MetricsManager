using MetricsAgent.DAL.Interfaces;
using Quartz;
using System.Reflection;
using System;
using System.Threading.Tasks;
using MetricsAgent.DTO;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;

namespace MetricsAgent.Schedule.Jobs
{
    public class RamMetricJob : IJob
    {
        private IRamMetricsRepository _repository;
        private PerformanceCounter _ramCounter;

        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var ramUsageInPrecents = _ramCounter.NextValue();
            var time = DateTimeOffset.UtcNow;

            await _repository.CreateAsync(new RamMetricDto { Time = time, Value = Convert.ToInt32(ramUsageInPrecents) });
        }
    }
}