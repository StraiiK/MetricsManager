using AutoMapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private IRamMetricsRepository _repository;
        private readonly IMapper _mapper;

        public RamMetricsController(IRamMetricsRepository repository, ILogger<RamMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug("NLog встроен в RamMetricsController");
            _mapper = mapper;
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = new AllRamMetricsApiResponse()
            {
                Metrics = await _repository.GetAllAsync(cancellationToken)
            };

            _logger.LogInformation($"Метод отработал");
            return Ok(response);
        }

        [HttpGet("get/agent")]
        public async Task<IActionResult> GetByPeriodFromAgentAsync([FromQuery] int agentId, [FromQuery] DateTimeOffset fromTime, [FromQuery] DateTimeOffset toTime, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Параметры метода:{@agentId_@fromTime}_{@toTime}", agentId, fromTime, toTime);

            var response = new AllRamMetricsApiResponse()
            {
                Metrics = await _repository.GetByPeriodFromAgentAsync(agentId, fromTime, toTime, cancellationToken)
            };

            _logger.LogInformation("Метод отработал");
            return Ok(response);
        }

        [HttpGet("get/cluster")]
        public async Task<IActionResult> GetByPeriodFromAllClusterAsync([FromQuery] DateTimeOffset fromTime, [FromQuery] DateTimeOffset toTime, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Параметры метода:{@fromTime}_{@toTime}", fromTime, toTime);

            var response = new AllRamMetricsApiResponse()
            {
                Metrics = await _repository.GetByPeriodFromAllClusterAsync(fromTime, toTime, cancellationToken)
            };

            _logger.LogInformation("Метод отработал");
            return Ok(response);
        }
    }
}
