﻿using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.DTO;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkAgentController : ControllerBase
    {
        private readonly ILogger<NetworkAgentController> _logger;
        private INetworkMetricsRepository _repository;

        public NetworkAgentController(INetworkMetricsRepository repository, ILogger<NetworkAgentController> logger)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetworkAgentController");
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            _repository.Create(new NetworkMetricModel
            {
                Time = request.Time,
                Value = request.Value
            });
            _logger.LogInformation($"Параметры метода:{request.Time}_{request.Value}");
            return Ok();
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();

            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var item in metrics)
            {
                response.Metrics.Add(new NetworkMetricDto
                {
                    Id = item.Id,
                    Value = item.Value,
                    Time = item.Time
                });
            }

            _logger.LogInformation($"Выполнен метод GetAll");
            return Ok(response);
        }

        [HttpGet("getbyperiod")]
        public IActionResult GetByPeriod([FromQuery] DateTimeOffset fromTime, [FromQuery] DateTimeOffset toTime)
        {
            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllNetworkMetricsResponse()
            {
                Metrics = metrics.Select(x => new NetworkMetricDto
                {
                    Id = x.Id,
                    Value = x.Value,
                    Time = x.Time
                }).ToList()
            };

            _logger.LogInformation($"Параметры метода:{fromTime}_{toTime}");
            return Ok(response);
        }
    }
}
