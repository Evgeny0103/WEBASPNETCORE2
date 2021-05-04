using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Responses
{
   
        public class GetByPeriodCpuMetricsResponse
        {
            public List<CpuMetricDto> Metrics { get; set; }
        }
    
}
