using AutoMapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DTO;
using MetricsManager.Requests;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

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
        public async Task<IActionResult> RegisterAgentAsync([FromBody] RegisterAgentRequest agentInfo, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Параметры метода:{@agentUrl}", agentInfo.AgentUrl);
            await _repository.CreateAsync(_mapper.Map<AgentDto>(agentInfo));
            return Ok();
        }

        [HttpGet("getall")]
        public async Task <IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = new AllAgentsResponse()
            {
                Agents = await _repository.GetAllAsync()
            };

            _logger.LogInformation($"Метод отработал");
            return Ok(response);
        }
    }
}
