﻿using AutoMapper;
using MetricsManager.Client;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DTO;
using MetricsManager.Requests;
using Quartz;
using System;
using System.Threading.Tasks;

namespace MetricsManager.Jobs
{
    public class RomMetricJob : IJob
    {
        private IRomMetricsRepository _repository;
        private IMetricsAgentClient _agentClient;
        private IAgentRepository _agentRepository;

        public RomMetricJob(IRomMetricsRepository repository, IMetricsAgentClient agentClient, IAgentRepository agentRepository)
        {
            _repository = repository;
            _agentClient = agentClient;
            _agentRepository = agentRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var AgentAddress = _agentRepository.GetAll();
            foreach (var agent in AgentAddress)
            {
                var fromTime = _repository.GetLastOfTime(agent.AgentId);
                var toTime = DateTimeOffset.UtcNow;

                var request = new GetAllRomMetricsApiRequest()
                {
                    AgentAddress = agent.AgentUrl,
                    FromTime = fromTime,
                    ToTime = toTime
                };

                var responseClient = _agentClient.GetRomMetrics(request);
                if (responseClient == null)
                {
                    return null;
                }

                foreach (var metrics in responseClient.Metrics)
                {
                    metrics.AgentId = agent.AgentId;
                    _repository.Create(metrics);
                }                                              
            }
            return Task.CompletedTask;
        }
    }
}