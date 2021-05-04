using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Responses
{
    public class GetByPeriodRamMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
}
