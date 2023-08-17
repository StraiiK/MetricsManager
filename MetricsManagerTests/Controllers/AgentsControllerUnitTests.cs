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
        private RegisterAgentRequest _agentInfo;
        public AgentsControllerUnitTests()
        {
            _controller = new AgentsController();
            _agentInfo = new RegisterAgentRequest();
        }

        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            var result = _controller.RegisterAgent(_agentInfo);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
