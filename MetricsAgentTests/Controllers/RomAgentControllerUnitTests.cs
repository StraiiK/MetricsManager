using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.DTO;
using MetricsAgent.Requests;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MetricsAgentTests.Controllers
{
    public class RomAgentControllerUnitTests
    {
        private Mock<IRomMetricsRepository> _mockRepo;
        private Mock<ILogger<RomAgentController>> _mockLog;
        private RomAgentController _controller;
        private Mock<IMapper> _mockMapper;

        public RomAgentControllerUnitTests()
        {
            _mockRepo = new Mock<IRomMetricsRepository>();
            _mockLog = new Mock<ILogger<RomAgentController>>();
            _mockMapper = new Mock<IMapper>();
            _controller = new RomAgentController(_mockRepo.Object, _mockLog.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Create_ShouldCall_Create_From_Repository()
        {
            _mockRepo.Setup(repository => repository.CreateAsync(It.IsAny<RomMetricDto>(), It.IsAny<CancellationToken>())).Verifiable();

            await _controller.CreateAsync(new RomMetricCreateRequest
            {
                Time = DateTimeOffset.FromFileTime(1),
                Value = 50
            });

            _mockRepo.Verify(repository => repository.CreateAsync(It.IsAny<RomMetricDto>(), It.IsAny<CancellationToken>()), Times.AtMostOnce());
            _mockRepo.Verify(repository => repository.CreateAsync(It.IsAny<RomMetricDto>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce());
        }

        [Fact]
        public async Task GetByPeriodFromAgent_ReturnsOk()
        {
            _mockRepo.Setup(repo => repo.GetByTimePeriodAsync(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<IList<RomMetricDto>>(new List<RomMetricDto>()));

            var result = await _controller.GetByPeriodAsync(DateTimeOffset.FromFileTime(1), DateTimeOffset.FromFileTime(100));

            _mockRepo.Verify(repo => repo.GetByTimePeriodAsync(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>()), Times.AtMostOnce());
            _mockRepo.Verify(repo => repo.GetByTimePeriodAsync(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce());
        }

        [Fact]
        public async Task GetAllFromAgent_ReturnsOk()
        {
            _mockRepo.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult<IList<RomMetricDto>>(new List<RomMetricDto>()));

            var result = await _controller.GetAllAsync();

            _mockRepo.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.AtMostOnce());
            _mockRepo.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce());
        }
    }
}