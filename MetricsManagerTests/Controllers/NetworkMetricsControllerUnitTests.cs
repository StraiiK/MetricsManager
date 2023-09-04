using System;
using Xunit;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DTO;
using System.Threading.Tasks;
using System.Threading;

namespace MetricsManagerTests.Controllers
{
    public class NetworkMetricsControllerUnitTests
    {
        private Mock<INetworkMetricsRepository> _mockRepository;
        private Mock<ILogger<NetworkMetricsController>> _mockLog;
        private Mock<IMapper> _moclMapper;
        private NetworkMetricsController _controller;

        public NetworkMetricsControllerUnitTests()
        {
            _mockRepository = new Mock<INetworkMetricsRepository>();
            _mockLog = new Mock<ILogger<NetworkMetricsController>>();
            _moclMapper = new Mock<IMapper>();
            _controller = new NetworkMetricsController(_mockRepository.Object, _mockLog.Object, _moclMapper.Object);
        }

        [Fact]
        public async Task GetByPeriodFromAgent_ReturnsOk()
        {
            _mockRepository.Setup(repo => repo.GetByPeriodFromAgentAsync(It.IsAny<int>(),
                It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<IList<NetworkMetricDto>>(new List<NetworkMetricDto>()));

            var result = await _controller.GetByPeriodFromAgentAsync(Int32.MinValue,
                DateTimeOffset.FromFileTime(1), DateTimeOffset.FromFileTime(100));

            _mockRepository.Verify(repo => repo.GetByPeriodFromAgentAsync(It.IsAny<int>(),
                It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>()), Times.AtMostOnce());
        }

        [Fact]
        public async Task GetByPeriodFromAllCluster_ReturnsOk()
        {
            _mockRepository.Setup(repo => repo.GetByPeriodFromAllClusterAsync(It.IsAny<DateTimeOffset>(),
                It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<IList<NetworkMetricDto>>(new List<NetworkMetricDto>()));

            var result = await _controller.GetByPeriodFromAllClusterAsync(DateTimeOffset.FromFileTime(1), DateTimeOffset.FromFileTime(100));

            _mockRepository.Verify(repo => repo.GetByPeriodFromAllClusterAsync(It.IsAny<DateTimeOffset>(),
                It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>()), Times.AtMostOnce());
        }

        [Fact]
        public async Task GetAllFromAgent_ReturnsOk()
        {
            _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult<IList<NetworkMetricDto>>(new List<NetworkMetricDto>()));

            var result = await _controller.GetAllAsync();

            _mockRepository.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.AtMostOnce());
        }
    }
}