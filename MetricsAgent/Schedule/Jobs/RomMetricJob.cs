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
        private PerformanceCounter _romCounter;

        public RomMetricJob(IRomMetricsRepository repository)
        {
            _repository = repository;
            _romCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var romUsageInPrecents = _romCounter.NextValue();
            var time = DateTimeOffset.UtcNow;

            await _repository.CreateAsync(new RomMetricDto { Time = time, Value = Convert.ToInt32(romUsageInPrecents) });
        }
    }
}