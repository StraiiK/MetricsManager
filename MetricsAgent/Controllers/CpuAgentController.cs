using AutoMapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DTO;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuAgentController : ControllerBase
    {
        private readonly ILogger<CpuAgentController> _logger;
        private ICpuMetricsRepository _repository;
        private readonly IMapper _mapper;

        public CpuAgentController(ICpuMetricsRepository repository, ILogger<CpuAgentController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug("NLog встроен в CpuAgentController");
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CpuMetricCreateRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Параметры метода:{@requestTime}_{@requestValue}", request.Time, request.Value);
            await _repository.CreateAsync(_mapper.Map<CpuMetricDto>(request));
            return Ok();
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = new AllCpuMetricsResponse()
            {
                Metrics = await _repository.GetAllAsync()
            };

            _logger.LogInformation($"Метод отработал");
            return Ok(response);
        }

        [HttpGet("getbyperiod")]
        public async Task<IActionResult> GetByPeriodAsync([FromQuery] DateTimeOffset fromTime, [FromQuery] DateTimeOffset toTime, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Параметры метода:{@fromTime}_{@toTime}", fromTime, toTime);

            var response = new AllCpuMetricsResponse()
            {
                Metrics = await _repository.GetByTimePeriodAsync(fromTime, toTime)
            };

            _logger.LogInformation("Метод отработал");
            return Ok(response);
        }
    }
}