using MetricsAgent.DAL.Interfaces;
using Quartz;
using System.Reflection;
using System;
using System.Threading.Tasks;
using MetricsAgent.DTO;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private IDotNetMetricsRepository _repository;
        private PerformanceCounter _DotNetCounter;

        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _DotNetCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var DotNetUsageInPrecents = _DotNetCounter.NextValue();
            var time = DateTimeOffset.UtcNow;

            _repository.Create(new DotNetMetricDto { Time = time, Value = Convert.ToInt32(DotNetUsageInPrecents) });

            return Task.CompletedTask;
        }
    }
}