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
    public class NetworkMetricJob : IJob
    {
        private INetworkMetricsRepository _repository;
        private PerformanceCounter _NetworkCounter;

        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            _NetworkCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var NetworkUsageInPrecents = _NetworkCounter.NextValue();
            var time = DateTimeOffset.UtcNow;

            _repository.Create(new NetworkMetricDto { Time = time, Value = Convert.ToInt32(NetworkUsageInPrecents) });

            return Task.CompletedTask;
        }
    }
}