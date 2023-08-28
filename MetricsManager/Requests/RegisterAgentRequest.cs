using System;

namespace MetricsManager.Requests
{
    public class RegisterAgentRequest
    {
        public int AgentId { get; set; }
        public string AgentUrl { get; set; }
    }
}
