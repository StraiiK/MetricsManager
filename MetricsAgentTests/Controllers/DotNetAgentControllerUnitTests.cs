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
    public class DotNetAgentControllerUnitTests
    {
        private Mock<IDotNetMetricsRepository> _mock;
        private Mock<ILogger<DotNetAgentController>> _mockLog;
        private DotNetAgentController _controller;
        private Mock<IMapper> _mapper;

        public DotNetAgentControllerUnitTests()
        {
            _mock = new Mock<IDotNetMetricsRepository>();
            _mockLog = new Mock<ILogger<DotNetAgentController>>();
            _mapper = new Mock<IMapper>();
            _controller = new DotNetAgentController(_mock.Object, _mockLog.Object, _mapper.Object);
        }

        [Fact]
        public async Task Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.CreateAsync(It.IsAny<DotNetMetricDto>(), It.IsAny<CancellationToken>())).Verifiable();

            await _controller.CreateAsync(new DotNetMetricCreateRequest
            {
                Time = DateTimeOffset.FromFileTime(1),
                Value = 50
            });

            _mock.Verify(repository => repository.CreateAsync(It.IsAny<DotNetMetricDto>(), It.IsAny<CancellationToken>()), Times.AtMostOnce());
        }

        [Fact]
        public async Task GetByPeriodFromAgent_ReturnsOk()
        {
            _mock.Setup(repo => repo.GetByTimePeriodAsync(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<IList<DotNetMetricDto>>(new List<DotNetMetricDto>()));

            var result = await _controller.GetByPeriodAsync(DateTimeOffset.FromFileTime(1), DateTimeOffset.FromFileTime(100));

            _mock.Verify(repo => repo.GetByTimePeriodAsync(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>()), Times.AtMostOnce());
        }

        [Fact]
        public async Task GetAllFromAgent_ReturnsOk()
        {
            _mock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult<IList<DotNetMetricDto>>(new List<DotNetMetricDto>()));

            var result = await _controller.GetAllAsync();

            _mock.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.AtMostOnce());
        }
    }
}