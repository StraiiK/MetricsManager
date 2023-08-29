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
    public class NetworkMetricJob : IJob
    {
        private INetworkMetricsRepository _repository;
        private PerformanceCounter _NetworkCounter;

        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            _NetworkCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", new PerformanceCounterCategory("Network Interface").GetInstanceNames()[0]);
        }

        public Task Execute(IJobExecutionContext context)
        {
            var NetworkUsage = _NetworkCounter.NextValue();
            var time = DateTimeOffset.UtcNow;

            _repository.Create(new NetworkMetricDto { Time = time, Value = Convert.ToInt32(NetworkUsage) });

            return Task.CompletedTask;
        }
    }
}