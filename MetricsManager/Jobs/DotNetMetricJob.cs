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
    public class DotNetMetricJob : IJob
    {
        private IDotNetMetricsRepository _repository;
        private IMetricsAgentClient _agentClient;
        private IAgentRepository _agentRepository;

        public DotNetMetricJob(IDotNetMetricsRepository repository, IMetricsAgentClient agentClient, IAgentRepository agentRepository)
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

                var request = new GetAllDotNetMetricsApiRequest()
                {
                    AgentAddress = agent.AgentUrl,
                    FromTime = fromTime,
                    ToTime = toTime
                };

                var responseClient = _agentClient.GetDotNetMetrics(request);
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