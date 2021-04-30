using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MetricsAgentTests.cs
{
    public class HddControllerUnitTests
    {
        private readonly HddMetricsController _controller;

        public HddControllerUnitTests()
        {
            _controller = new HddMetricsController();
        }


        [Fact]
        public void GetRemainingFreeDiskSpaceMetrics_ReturnsOk()
        {
            var result = _controller.GetRemainingFreeDiskSpaceMetrics();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
