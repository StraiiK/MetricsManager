using AutoMapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DTO;
using MetricsManager.Requests;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MetricsManager.Controllers
{
    [Route("api/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private IMapper _mapper;
        private IAgentRepository _repository;

        public AgentsController(ILogger<CpuMetricsController> logger, IMapper mapper, IAgentRepository repository)
        {            
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] RegisterAgentRequest agentInfo)
        {
            _logger.LogInformation("Параметры метода:{@agentUrl}", agentInfo.AgentUrl);
            _repository.Create(_mapper.Map<AgentDto>(agentInfo));
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var response = new AllAgentsResponse()
            {
                Agents = _repository.GetAll()
            };

            _logger.LogInformation($"Метод отработал");
            return Ok(response);
        }
    }
}
