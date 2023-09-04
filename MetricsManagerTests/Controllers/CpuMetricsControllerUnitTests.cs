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

namespace MetricsManagerTests.Controllers
{
    public class CpuMetricsControllerUnitTests
    {
        //private Mock<ICpuMetricsRepository> _mockRepository;
        //private Mock<ILogger<CpuMetricsController>> _mockLog;        
        //private Mock<IMapper> _moclMapper;
        //private CpuMetricsController _controller;

        //public CpuMetricsControllerUnitTests()
        //{
        //    _mockRepository = new Mock<ICpuMetricsRepository>();
        //    _mockLog = new Mock<ILogger<CpuMetricsController>>();
        //    _moclMapper = new Mock<IMapper>();
        //    _controller = new CpuMetricsController(_mockRepository.Object, _mockLog.Object, _moclMapper.Object);
        //}

        //[Fact]
        //public void GetByPeriodFromAgent_ReturnsOk()
        //{
        //    _mockRepository.Setup(repo => repo.GetByPeriodFromAgentAsync(It.IsAny<int>(), 
        //        It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<CpuMetricDto>());

        //    var result = _controller.GetByPeriodFromAgentAsync(Int32.MinValue, 
        //        DateTimeOffset.FromFileTime(1), DateTimeOffset.FromFileTime(100));

        //    _mockRepository.Verify(repo => repo.GetByPeriodFromAgentAsync(It.IsAny<int>(), 
        //        It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        //}

        //[Fact]
        //public void GetByPeriodFromAllCluster_ReturnsOk()
        //{
        //    _mockRepository.Setup(repo => repo.GetByPeriodFromAllClusterAsync(It.IsAny<DateTimeOffset>(), 
        //        It.IsAny<DateTimeOffset>())).Returns(new List<CpuMetricDto>());

        //    var result = _controller.GetByPeriodFromAllClusterAsync(DateTimeOffset.FromFileTime(1), DateTimeOffset.FromFileTime(100));

        //    _mockRepository.Verify(repo => repo.GetByPeriodFromAllClusterAsync(It.IsAny<DateTimeOffset>(), 
        //        It.IsAny<DateTimeOffset>()), Times.AtMostOnce());
        //}

        //[Fact]
        //public void GetAllFromAgent_ReturnsOk()
        //{
        //    _mockRepository.Setup(repo => repo.GetAllAsync()).Returns(new List<CpuMetricDto>());

        //    var result = _controller.GetAllAsync();

        //    _mockRepository.Verify(repo => repo.GetAllAsync(), Times.AtMostOnce());
        //}
    }
}