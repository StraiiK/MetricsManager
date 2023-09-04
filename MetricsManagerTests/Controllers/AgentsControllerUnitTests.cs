using AutoMapper;
using MetricsManager.Controllers;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DTO;
using MetricsManager.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MetricsManagerTests.Controllers
{
    public class AgentsControllerUnitTests
    {
        private Mock<IAgentRepository> _mockRepository;
        private Mock<ILogger<AgentsController>> _mockLog;
        private Mock<IMapper> _mockMapper;
        private AgentsController _controller;

        public AgentsControllerUnitTests()
        {
            _mockRepository = new Mock<IAgentRepository>();
            _mockLog = new Mock<ILogger<AgentsController>>();
            _mockMapper = new Mock<IMapper>();
            _controller = new AgentsController(_mockLog.Object, _mockMapper.Object, _mockRepository.Object);
        }

        [Fact]
        public async Task RegisterAgentAsync_ReturnsOk()
        {
            _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<AgentDto>(), It.IsAny<CancellationToken>())).Verifiable();

            var result = await _controller.RegisterAgentAsync(new RegisterAgentRequest
            {
                AgentId = 1,
                AgentUrl = "url123"
            });

            _mockRepository.Verify(repo => repo.CreateAsync(It.IsAny<AgentDto>(), It.IsAny<CancellationToken>()), Times.AtMostOnce());
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOk()
        {
            _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult<IList<AgentDto>>(new List<AgentDto>()));

            var result = await _controller.GetAllAsync();

            _mockRepository.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.AtMostOnce());
        }
    }
}
