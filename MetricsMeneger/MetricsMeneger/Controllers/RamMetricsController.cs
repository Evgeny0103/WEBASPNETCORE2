using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsMeneger.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;

        public RamMetricsController(ILogger<RamMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamMetricsController");
        }

        [HttpGet("agent/{agentId}/available")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int agentId)
        {
            _logger.LogInformation($"Агент: {agentId}");
            return Ok();
        }


        [HttpGet("cluster/available")]
        public IActionResult GetMetricsFromAllCluster()
        {
            _logger.LogInformation($"Запрос общих данных");
            return Ok();
        }
    }
}
