﻿using MetricsAgent.DAL.Interfaces;
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
    public class RamMetricJob : IJob
    {
        private IRamMetricsRepository _repository;
        private PerformanceCounter _RamCounter;

        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _RamCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var RamUsageInPrecents = _RamCounter.NextValue();
            var time = DateTimeOffset.UtcNow;

            _repository.Create(new RamMetricDto { Time = time, Value = Convert.ToInt32(RamUsageInPrecents) });

            return Task.CompletedTask;
        }
    }
}