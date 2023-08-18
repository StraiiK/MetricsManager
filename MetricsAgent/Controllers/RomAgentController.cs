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
using System.Linq;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/rom")]
    [ApiController]
    public class RomAgentController : ControllerBase
    {
        private readonly ILogger<RomAgentController> _logger;
        private IRomMetricsRepository _repository;

        public RomAgentController(IRomMetricsRepository repository, ILogger<RomAgentController> logger)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RomAgentController");
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RomMetricCreateRequest request)
        {
            _repository.Create(new RomMetricModel
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

            var response = new AllRomMetricsResponse()
            {
                Metrics = new List<RomMetricDto>()
            };

            foreach (var item in metrics)
            {
                response.Metrics.Add(new RomMetricDto
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

            var response = new AllRomMetricsResponse()
            {
                Metrics = metrics.Select(x => new RomMetricDto
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
