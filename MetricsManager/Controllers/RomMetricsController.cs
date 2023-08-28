using AutoMapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/rom")]
    [ApiController]
    public class RomMetricsController : ControllerBase
    {
        private readonly ILogger<RomMetricsController> _logger;
        private IRomMetricsRepository _repository;
        private readonly IMapper _mapper;

        public RomMetricsController(IRomMetricsRepository repository, ILogger<RomMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug("NLog встроен в RomMetricsController");
            _mapper = mapper;
        }

        [HttpGet("get/all")]
        public IActionResult GetAll()
        {
            var response = new AllRomMetricsApiResponse()
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

            var response = new AllRomMetricsApiResponse()
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

            var response = new AllRomMetricsApiResponse()
            {
                Metrics = _repository.GetByPeriodFromAllCluster(fromTime, toTime)
            };

            _logger.LogInformation("Метод отработал");
            return Ok(response);
        }
    }
}
