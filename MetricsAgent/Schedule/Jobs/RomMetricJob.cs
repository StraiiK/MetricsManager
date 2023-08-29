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
    public class RomMetricJob : IJob
    {
        private IRomMetricsRepository _repository;
        private PerformanceCounter _RomCounter;

        public RomMetricJob(IRomMetricsRepository repository)
        {
            _repository = repository;
            _RomCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var RomUsage = _RomCounter.NextValue();
            var time = DateTimeOffset.UtcNow;

            _repository.Create(new RomMetricDto { Time = time, Value = Convert.ToInt32(RomUsage) });

            return Task.CompletedTask;
        }
    }
}