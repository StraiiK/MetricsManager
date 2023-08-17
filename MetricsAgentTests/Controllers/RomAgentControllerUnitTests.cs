﻿using MetricsAgent.Controllers;
using MetricsAgent.DAL.InterfaceDal;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MetricsAgentTests.Controllers
{
    public class RomAgentControllerUnitTests
    {
        private Mock<IRomMetricsRepository> mock;
        private Mock<ILogger<RomAgentController>> mockLog;
        private RomAgentController _controller;

        public RomAgentControllerUnitTests()
        {
            mock = new Mock<IRomMetricsRepository>();
            mockLog = new Mock<ILogger<RomAgentController>>();
            _controller = new RomAgentController(mock.Object, mockLog.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            mock.Setup(repository => repository.Create(It.IsAny<BaseMetricModel>())).Verifiable();

            _controller.Create(new BaseMetricCreateRequest
            {
                Time = DateTimeOffset.FromFileTime(1),
                Value = 50
            });

            mock.Verify(repository => repository.Create(It.IsAny<BaseMetricModel>()), Times.AtMostOnce());
        }

        [Fact]
        public void GetByPeriodFromAgent_ReturnsOk()
        {
            mock.Setup(repo => repo.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            var result = _controller.GetByPeriod(new BaseMetricGetByPeriodRequest()
            {
                fromTime = DateTimeOffset.FromFileTime(1),
                toTime = DateTimeOffset.FromFileTime(100)
            });

            mock.Verify(repo => repo.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        }

        [Fact]
        public void GetAllFromAgent_ReturnsOk()
        {
            mock.Setup(repo => repo.GetAll()).Verifiable();

            var result = _controller.GetAll();

            mock.Verify(repo => repo.GetAll(), Times.AtMostOnce());
        }
    }
}
