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
    public class RamAgentControllerUnitTests
    {
        private Mock<IRamMetricsRepository> _mock;
        private Mock<ILogger<RamAgentController>> _mockLog;
        private RamAgentController _controller;

        public RamAgentControllerUnitTests()
        {
            _mock = new Mock<IRamMetricsRepository>();
            _mockLog = new Mock<ILogger<RamAgentController>>();
            _controller = new RamAgentController(_mock.Object, _mockLog.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<RamMetricModel>())).Verifiable();

            _controller.Create(new RamMetricCreateRequest
            {
                Time = DateTimeOffset.FromFileTime(1),
                Value = 50
            });

            _mock.Verify(repository => repository.Create(It.IsAny<RamMetricModel>()), Times.AtMostOnce());
        }

        [Fact]
        public void GetByPeriodFromAgent_ReturnsOk()
        {
            _mock.Setup(repo => repo.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<RamMetricModel>());

            var result = _controller.GetByPeriod(DateTimeOffset.FromFileTime(1), DateTimeOffset.FromFileTime(100));


            _mock.Verify(repo => repo.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }

        [Fact]
        public void GetAllFromAgent_ReturnsOk()
        {
            _mock.Setup(repo => repo.GetAll()).Returns(new List<RamMetricModel>());

            var result = _controller.GetAll();

            _mock.Verify(repo => repo.GetAll(), Times.AtMostOnce());
        }
    }
}
