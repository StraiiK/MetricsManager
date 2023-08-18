using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.DTO;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetAgentController : ControllerBase
    {
        private readonly ILogger<DotNetAgentController> _logger;
        private IDotNetMetricsRepository _repository;

        public DotNetAgentController(IDotNetMetricsRepository repository, ILogger<DotNetAgentController> logger)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в DotNetAgentController");
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
        {
            _repository.Create(new DotNetMetricModel
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

            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };

            foreach (var item in metrics)
            {
                response.Metrics.Add(new DotNetMetricDto
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

            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };

            foreach (var item in metrics)
            {
                response.Metrics.Add(new DotNetMetricDto
                {
                    Id = item.Id,
                    Value = item.Value,
                    Time = item.Time
                });
            }


            _logger.LogInformation($"Параметры метода:{fromTime}_{toTime}");
            return Ok(response);
        }
    }
}
