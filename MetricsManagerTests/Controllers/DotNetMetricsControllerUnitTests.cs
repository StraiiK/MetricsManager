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
    public class DotNetMetricsControllerUnitTests
    {
        private Mock<IDotNetMetricsRepository> _mockRepository;
        private Mock<ILogger<DotNetMetricsController>> _mockLog;
        private Mock<IMapper> _moclMapper;
        private DotNetMetricsController _controller;

        public DotNetMetricsControllerUnitTests()
        {
            _mockRepository = new Mock<IDotNetMetricsRepository>();
            _mockLog = new Mock<ILogger<DotNetMetricsController>>();
            _moclMapper = new Mock<IMapper>();
            _controller = new DotNetMetricsController(_mockRepository.Object, _mockLog.Object, _moclMapper.Object);
        }

        [Fact]
        public async Task GetByPeriodFromAgent_ReturnsOk()
        {
            _mockRepository.Setup(repo => repo.GetByPeriodFromAgentAsync(It.IsAny<int>(),
                It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<IList<DotNetMetricDto>>(new List<DotNetMetricDto>()));

            var result = await _controller.GetByPeriodFromAgentAsync(Int32.MinValue,
                DateTimeOffset.FromFileTime(1), DateTimeOffset.FromFileTime(100));

            _mockRepository.Verify(repo => repo.GetByPeriodFromAgentAsync(It.IsAny<int>(),
                It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>()), Times.AtMostOnce());
        }

        [Fact]
        public async Task GetByPeriodFromAllCluster_ReturnsOk()
        {
            _mockRepository.Setup(repo => repo.GetByPeriodFromAllClusterAsync(It.IsAny<DateTimeOffset>(),
                It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<IList<DotNetMetricDto>>(new List<DotNetMetricDto>()));

            var result = await _controller.GetByPeriodFromAllClusterAsync(DateTimeOffset.FromFileTime(1), DateTimeOffset.FromFileTime(100));

            _mockRepository.Verify(repo => repo.GetByPeriodFromAllClusterAsync(It.IsAny<DateTimeOffset>(),
                It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>()), Times.AtMostOnce());
        }

        [Fact]
        public async Task GetAllFromAgent_ReturnsOk()
        {
            _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult<IList<DotNetMetricDto>>(new List<DotNetMetricDto>()));

            var result = await _controller.GetAllAsync();

            _mockRepository.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.AtMostOnce());
        }
    }
}