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
        public IActionResult Create([FromBody] RomMetricCreateRequest request)
        {
            _logger.LogInformation("Параметры метода:{@requestTime}_{@requestValue}", request.Time, request.Value);
            _repository.Create(_mapper.Map<RomMetricDto>(request));
            return Ok();
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();

            var response = new AllRomMetricsResponse()
            {
                Metrics = _mapper.Map<List<RomMetricDto>>(metrics)
            };

            _logger.LogInformation($"Метод отработал");
            return Ok(response);
        }

        [HttpGet("getbyperiod")]
        public IActionResult GetByPeriod([FromQuery] DateTimeOffset fromTime, [FromQuery] DateTimeOffset toTime)
        {
            _logger.LogInformation("Параметры метода:{@fromTime}_{@toTime}", fromTime, toTime);
            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllRomMetricsResponse()
            {
                Metrics = _mapper.Map<List<RomMetricDto>>(metrics)
            };

            _logger.LogInformation("Метод отработал");
            return Ok(response);
        }
    }
}
