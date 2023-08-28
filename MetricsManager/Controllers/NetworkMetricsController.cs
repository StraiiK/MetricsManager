using AutoMapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private INetworkMetricsRepository _repository;
        private readonly IMapper _mapper;

        public NetworkMetricsController(INetworkMetricsRepository repository, ILogger<NetworkMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug("NLog встроен в NetworkMetricsController");
            _mapper = mapper;
        }

        [HttpGet("get/all")]
        public IActionResult GetAll()
        {
            var response = new AllNetworkMetricsApiResponse()
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

            var response = new AllNetworkMetricsApiResponse()
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

            var response = new AllNetworkMetricsApiResponse()
            {
                Metrics = _repository.GetByPeriodFromAllCluster(fromTime, toTime)
            };

            _logger.LogInformation("Метод отработал");
            return Ok(response);
        }
    }
}
