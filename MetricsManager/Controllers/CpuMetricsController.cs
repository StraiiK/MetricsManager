using AutoMapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private ICpuMetricsRepository _repository;
        private readonly IMapper _mapper;

        public CpuMetricsController(ICpuMetricsRepository repository, ILogger<CpuMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug("NLog встроен в CpuMetricsController");
            _mapper = mapper;
        }

        [HttpGet("get/all")]
        public IActionResult GetAll()
        {
            var response = new AllCpuMetricsApiResponse()
            {
                Metrics = _repository.GetAll()
            };

            _logger.LogInformation($"Метод отработал");
            return Ok(response);
        }

        [HttpGet("get/agent")]
        public IActionResult GetByPeriodFromAgent([FromQuery] int agentId, [FromQuery] DateTimeOffset fromTime, [FromQuery] DateTimeOffset toTime)        
        {
            _logger.LogInformation("Параметры метода:{@agentId_@fromTime}_{@toTime}", agentId, fromTime, toTime);

            var response = new AllCpuMetricsApiResponse()
            {
                Metrics = _repository.GetByPeriodFromAgent(agentId, fromTime, toTime)
            };

            _logger.LogInformation("Метод отработал");
            return Ok(response);
        }

        [HttpGet("get/cluster")]
        public IActionResult GetByPeriodFromAllCluster([FromQuery] DateTimeOffset fromTime, [FromQuery] DateTimeOffset toTime)
        {
            _logger.LogInformation("Параметры метода:{@fromTime}_{@toTime}", fromTime, toTime);

            var response = new AllCpuMetricsApiResponse()
            {
                Metrics = _repository.GetByPeriodFromAllCluster(fromTime, toTime)
            };

            _logger.LogInformation("Метод отработал");
            return Ok(response);
        }
    }
}
