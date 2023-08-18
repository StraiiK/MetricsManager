﻿using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.Requests;
using MetricsAgent.Requests.CreateMetric;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MetricsAgentTests.Controllers
{
    public class DotNetAgentControllerUnitTests
    {
        private Mock<IDotNetMetricsRepository> _mock;
        private Mock<ILogger<DotNetAgentController>> _mockLog;
        private DotNetAgentController _controller;

        public DotNetAgentControllerUnitTests()
        {
            _mock = new Mock<IDotNetMetricsRepository>();
            _mockLog = new Mock<ILogger<DotNetAgentController>>();
            _controller = new DotNetAgentController(_mock.Object, _mockLog.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<DotNetMetricModel>())).Verifiable();

            _controller.Create(new DotNetMetricCreateRequest
            {
                Time = DateTimeOffset.FromFileTime(1),
                Value = 50
            });

            _mock.Verify(repository => repository.Create(It.IsAny<DotNetMetricModel>()), Times.AtMostOnce());
        }

        [Fact]
        public void GetByPeriodFromAgent_ReturnsOk()
        {
            _mock.Setup(repo => repo.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<DotNetMetricModel>());

            var result = _controller.GetByPeriod(DateTimeOffset.FromFileTime(1), DateTimeOffset.FromFileTime(100));


            _mock.Verify(repo => repo.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }

        [Fact]
        public void GetAllFromAgent_ReturnsOk()
        {
            _mock.Setup(repo => repo.GetAll()).Returns(new List<DotNetMetricModel>());

            var result = _controller.GetAll();

            _mock.Verify(repo => repo.GetAll(), Times.AtMostOnce());
        }
    }
}
