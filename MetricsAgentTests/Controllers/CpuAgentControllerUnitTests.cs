using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.Requests;
using MetricsAgent.Requests.CreateMetric;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NLog;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTests.Controllers
{
    public class CpuAgentControllerUnitTests
    {
        private Mock<ICpuMetricsRepository> _mock;
        private Mock<ILogger<CpuAgentController>> _mockLog;
        private CpuAgentController _controller;

        public CpuAgentControllerUnitTests()
        {
            _mock = new Mock<ICpuMetricsRepository>();
            _mockLog = new Mock<ILogger<CpuAgentController>>();
            _controller = new CpuAgentController(_mock.Object, _mockLog.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<CpuMetricModel>())).Verifiable();            

           _controller.Create(new CpuMetricCreateRequest
           {
                Time = DateTimeOffset.FromFileTime(1),
                Value = 50
            });

            _mock.Verify(repository => repository.Create(It.IsAny<CpuMetricModel>()), Times.AtMostOnce());
        }

        [Fact]
        public void GetByPeriodFromAgent_ReturnsOk()
        {
            _mock.Setup(repo => repo.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<CpuMetricModel>());

            var result = _controller.GetByPeriod(DateTimeOffset.FromFileTime(1), DateTimeOffset.FromFileTime(100));
           

            _mock.Verify(repo => repo.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }

        [Fact]
        public void GetAllFromAgent_ReturnsOk()
        {
            _mock.Setup(repo => repo.GetAll()).Returns(new List<CpuMetricModel>());
            
            var result = _controller.GetAll();

            _mock.Verify(repo => repo.GetAll(), Times.AtMostOnce());
        }
    }
}
