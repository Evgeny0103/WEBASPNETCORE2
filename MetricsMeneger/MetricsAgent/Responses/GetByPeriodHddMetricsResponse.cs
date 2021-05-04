using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Responses
{
    public class GetByPeriodHddMetricsResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
}
