using MetricsManager.DTO;
using System.Collections.Generic;

namespace MetricsManager.DAL.Interfaces
{
    public interface IAgentRepository
    {
        public void Create(AgentDto item);
        IList<AgentDto> GetAll();
    }
}
