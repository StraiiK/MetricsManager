using MetricsManager.DTO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Interfaces
{
    public interface IAgentRepository : IDisposable
    {
        public Task CreateAsync(AgentDto item, CancellationToken cancellationToken = default);
        Task <IList<AgentDto>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
