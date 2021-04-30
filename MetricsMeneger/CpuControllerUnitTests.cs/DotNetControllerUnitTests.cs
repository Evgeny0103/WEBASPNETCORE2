using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MetricsAgentTests.cs
{
  public  class DotNetControllerUnitTests
    {
        private readonly DotNetMetricsController _controller;

        public DotNetControllerUnitTests()
        {
            _controller = new DotNetMetricsController();
        }


        [Fact]
        public void GetErrorsCountMetrics_ReturnsOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            var result = _controller.GetErrorsCountMetrics(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}

