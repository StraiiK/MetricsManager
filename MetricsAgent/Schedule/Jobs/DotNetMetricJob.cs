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
    public class DotNetMetricJob : IJob
    {
        private IDotNetMetricsRepository _repository;
        private PerformanceCounter _dotNetCounter;

        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _dotNetCounter = new PerformanceCounter(".NET CLR Memory", "# Bytes in all Heaps", "_Global_");
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var dotNetUsageInPrecents = _dotNetCounter.NextValue();
            var time = DateTimeOffset.UtcNow;

            await _repository.CreateAsync(new DotNetMetricDto {Time = time, Value = Convert.ToInt32(dotNetUsageInPrecents)});                       
        }
    }
}