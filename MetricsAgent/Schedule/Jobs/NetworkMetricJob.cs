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
        private PerformanceCounter _networkCounter;

        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            _networkCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", new PerformanceCounterCategory("Network Interface").GetInstanceNames()[0]);
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var networkUsageInPrecents = _networkCounter.NextValue();
            var time = DateTimeOffset.UtcNow;

            await _repository.CreateAsync(new NetworkMetricDto { Time = time, Value = Convert.ToInt32(networkUsageInPrecents) });
        }
    }
}