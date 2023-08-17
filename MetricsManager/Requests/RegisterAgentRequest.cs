using System;

namespace MetricsManager.Requests
{
    public class RegisterAgentRequest
    {
        public int AgentId { get; }
        public Uri AgentAdress { get; }
    }
}
