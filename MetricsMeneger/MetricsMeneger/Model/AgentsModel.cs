using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsMeneger.Model
{
    public class AgentsModel
    {
        private readonly List<AgentInfo> _data;

        public AgentsModel()
        {
            _data = new List<AgentInfo>();
        }

        public IReadOnlyList<AgentInfo> GetAgentsInfo()
        {
            return _data;
        }
    }
}
