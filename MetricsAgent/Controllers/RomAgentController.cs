using AutoMapper;
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
using System.Threading.Tasks;
using System.Threading;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/rom")]
    [ApiController]
    public class RomAgentController : ControllerBase
    {
        private readonly ILogger<RomAgentController> _logger;
        private IRomMetricsRepository _repository;
        private readonly IMapper _mapper;

        public RomAgentController(IRomMetricsRepository repository, ILogger<RomAgentController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RomAgentController");
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] RomMetricCreateRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Параметры метода:{@requestTime}_{@requestValue}", request.Time, request.Value);
            await _repository.CreateAsync(_mapper.Map<RomMetricDto>(request));
            return Ok();
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = new AllRomMetricsResponse()
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

            var response = new AllRomMetricsResponse()
            {
                Metrics = await _repository.GetByTimePeriodAsync(fromTime, toTime)
            };

            _logger.LogInformation("Метод отработал");
            return Ok(response);
        }
    }
}
