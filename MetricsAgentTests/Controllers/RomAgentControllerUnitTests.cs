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
using Xunit;

namespace MetricsAgentTests.Controllers
{
    public class RomAgentControllerUnitTests
    {
        //private Mock<IRomMetricsRepository> _mock;
        //private Mock<ILogger<RomAgentController>> _mockLog;
        //private RomAgentController _controller;
        //private Mock<IMapper> _mapper;

        //public RomAgentControllerUnitTests()
        //{
        //    _mock = new Mock<IRomMetricsRepository>();
        //    _mockLog = new Mock<ILogger<RomAgentController>>();
        //    _mapper = new Mock<IMapper>();
        //    _controller = new RomAgentController(_mock.Object, _mockLog.Object, _mapper.Object);
        //}

        //[Fact]
        //public void Create_ShouldCall_Create_From_Repository()
        //{
        //    _mock.Setup(repository => repository.CreateAsync(It.IsAny<RomMetricDto>())).Verifiable();

        //    _controller.Create(new RomMetricCreateRequest
        //    {
        //        Time = DateTimeOffset.FromFileTime(1),
        //        Value = 50
        //    });

        //    _mock.Verify(repository => repository.CreateAsync(It.IsAny<RomMetricDto>()), Times.AtMostOnce());
        //}

        //[Fact]
        //public void GetByPeriodFromAgent_ReturnsOk()
        //{
        //    _mock.Setup(repo => repo.GetByTimePeriodAsync(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<RomMetricDto>());

        //    var result = _controller.GetByPeriod(DateTimeOffset.FromFileTime(1), DateTimeOffset.FromFileTime(100));


        //    _mock.Verify(repo => repo.GetByTimePeriodAsync(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        //}

        //[Fact]
        //public void GetAllFromAgent_ReturnsOk()
        //{
        //    _mock.Setup(repo => repo.GetAllAsync()).Returns(new List<RomMetricDto>());

        //    var result = _controller.GetAll();

        //    _mock.Verify(repo => repo.GetAllAsync(), Times.AtMostOnce());
        //}
    }
}
