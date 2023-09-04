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
    public class NetworkAgentControllerUnitTests
    {
        private Mock<INetworkMetricsRepository> _mock;
        private Mock<ILogger<NetworkAgentController>> _mockLog;
        private NetworkAgentController _controller;
        private Mock<IMapper> _mapper;

        public NetworkAgentControllerUnitTests()
        {
            _mock = new Mock<INetworkMetricsRepository>();
            _mockLog = new Mock<ILogger<NetworkAgentController>>();
            _mapper = new Mock<IMapper>();
            _controller = new NetworkAgentController(_mock.Object, _mockLog.Object, _mapper.Object);
        }

        [Fact]
        public async Task Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.CreateAsync(It.IsAny<NetworkMetricDto>(), It.IsAny<CancellationToken>())).Verifiable();

            await _controller.CreateAsync(new NetworkMetricCreateRequest
            {
                Time = DateTimeOffset.FromFileTime(1),
                Value = 50
            });

            _mock.Verify(repository => repository.CreateAsync(It.IsAny<NetworkMetricDto>(), It.IsAny<CancellationToken>()), Times.AtMostOnce());
        }

        [Fact]
        public async Task GetByPeriodFromAgent_ReturnsOk()
        {
            _mock.Setup(repo => repo.GetByTimePeriodAsync(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<IList<NetworkMetricDto>>(new List<NetworkMetricDto>()));

            var result = await _controller.GetByPeriodAsync(DateTimeOffset.FromFileTime(1), DateTimeOffset.FromFileTime(100));

            _mock.Verify(repo => repo.GetByTimePeriodAsync(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>()), Times.AtMostOnce());
        }

        [Fact]
        public async Task GetAllFromAgent_ReturnsOk()
        {
            _mock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult<IList<NetworkMetricDto>>(new List<NetworkMetricDto>()));

            var result = await _controller.GetAllAsync();

            _mock.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.AtMostOnce());
        }
    }
}