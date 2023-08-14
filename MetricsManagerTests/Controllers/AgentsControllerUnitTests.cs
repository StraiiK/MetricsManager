using MetricsManager.Controllers;
using MetricsManager.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MetricsManagerTests.Controllers
{
    public class AgentsControllerUnitTests
    {
        private AgentsController _controller;
        private AgentInfo _agentInfo;
        public AgentsControllerUnitTests()
        {
            _controller = new AgentsController();
            _agentInfo = new AgentInfo();
        }

        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            var result = _controller.RegisterAgent(_agentInfo);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
