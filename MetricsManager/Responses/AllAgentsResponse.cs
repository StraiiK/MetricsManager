using MetricsManager.DTO;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class AllAgentsResponse
    {
        public IList<AgentDto> Agents { get; set; }
    }
}
