using AutoMapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DTO;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

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
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _logger.LogInformation("Параметры метода:{@requestTime}_{@requestValue}", request.Time, request.Value);
            _repository.Create(_mapper.Map<CpuMetricDto>(request));
            return Ok();
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();

            var response = new AllCpuMetricsResponse()
            {
                Metrics = _mapper.Map<List<CpuMetricDto>>(metrics)
            };

            _logger.LogInformation($"Метод отработал");
            return Ok(response);
        }

        [HttpGet("getbyperiod")]
        public IActionResult GetByPeriod([FromQuery] DateTimeOffset fromTime, [FromQuery] DateTimeOffset toTime)
        {
            _logger.LogInformation("Параметры метода:{@fromTime}_{@toTime}", fromTime, toTime);
            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllCpuMetricsResponse()
            {
                Metrics = _mapper.Map<List<CpuMetricDto>>(metrics)
            };

            _logger.LogInformation("Метод отработал");
            return Ok(response);
        }
    }
}