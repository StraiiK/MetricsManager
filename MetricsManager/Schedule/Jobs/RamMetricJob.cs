﻿using AutoMapper;
using MetricsManager.Client;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DTO;
using MetricsManager.Requests;
using Quartz;
using System;
using System.Threading.Tasks;

namespace MetricsManager.Schedule.Jobs
{
    public class RamMetricJob : IJob
    {
        private IRamMetricsRepository _repository;
        private IMetricsAgentClient _agentClient;
        private IAgentRepository _agentRepository;

        public RamMetricJob(IRamMetricsRepository repository, IMetricsAgentClient agentClient, IAgentRepository agentRepository)
        {
            _repository = repository;
            _agentClient = agentClient;
            _agentRepository = agentRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var AgentAddress = await _agentRepository.GetAllAsync();
            foreach (var agent in AgentAddress)
            {
                var fromTime = await _repository.GetLastOfTimeAsync(agent.AgentId);
                var toTime = DateTimeOffset.UtcNow;

                var request = new GetAllRamMetricsApiRequest()
                {
                    AgentAddress = agent.AgentUrl,
                    FromTime = fromTime,
                    ToTime = toTime
                };

                var responseClient = await _agentClient.GetRamMetricsAsync(request);
                if (responseClient == null)
                {
                    return;
                }

                foreach (var metrics in responseClient.Metrics)
                {
                    metrics.AgentId = agent.AgentId;
                    await _repository.CreateAsync(metrics);
                }
            }

        }
    }
}