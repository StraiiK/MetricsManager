using MetricsAgent.DAL.InterfaceDal;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamAgentController : ControllerBase
    {
        private readonly ILogger<RamAgentController> _logger;
        private IRamMetricsRepository _repository;

        public RamAgentController(IRamMetricsRepository repository, ILogger<RamAgentController> logger)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamAgentController");
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] BaseMetricCreateRequest request)
        {
            _repository.Create(new BaseMetricModel
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

            var response = new AllBaseMetricsResponse()
            {
                Metrics = new List<BaseMetricDto>()
            };

            if (metrics != null)
            {
                foreach (var item in metrics)
                {
                    response.Metrics.Add(new BaseMetricDto
                    {
                        Id = item.Id,
                        Value = item.Value,
                        Time = item.Time
                    });
                }
            }

            _logger.LogInformation($"Выполнен метод GetAll");
            return Ok(response);
        }

        [HttpGet("getbytime")]
        public IActionResult GetByPeriod([FromBody] BaseMetricGetByPeriodRequest request)
        {
            var metrics = _repository.GetByTimePeriod(request.fromTime, request.toTime);

            var response = new AllBaseMetricsResponse()
            {
                Metrics = new List<BaseMetricDto>()
            };
            if (metrics != null)
            {
                foreach (var item in metrics)
                {
                    response.Metrics.Add(new BaseMetricDto
                    {
                        Id = item.Id,
                        Value = item.Value,
                        Time = item.Time
                    });
                }
            }

            _logger.LogInformation($"Параметры метода:{request.fromTime}_{request.toTime}");
            return Ok(response);
        }
    }
}
