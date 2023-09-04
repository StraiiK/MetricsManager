using AutoMapper;
using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Repositories
{
    public class AgentRepository : IAgentRepository
    {
        private IConnectionManager _connectionManager;
        private IMapper _mapper;
        private MetricsDbContext _dbContext;

        public AgentRepository(IConnectionManager connectionManager, IMapper mapper, MetricsDbContext dbContext)
        {
            _connectionManager = connectionManager;
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task CreateAsync(AgentDto item, CancellationToken cancellationToken = default)
        {
            await _dbContext.Agent.AddAsync(_mapper.Map<AgentDal>(item), cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<AgentDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.Agent.ToListAsync(cancellationToken);
            return _mapper.Map<IList<AgentDto>>(result);
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
