using AutoMapper;
using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.DTO;
using System.Collections.Generic;
using System.Linq;

namespace MetricsManager.DAL.Repositories
{
    public class AgentRepository : IAgentRepository
    {
        private IConnectionManager _connectionManager;
        private IMapper _mapper;

        public AgentRepository(IConnectionManager connectionManager, IMapper mapper)
        {
            _connectionManager = connectionManager;
            _mapper = mapper;
        }
        public void Create(AgentDto item)
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var metrics = _mapper.Map<AgentDal>(item);
                connection.Execute("INSERT INTO Agents(AgentUrl) VALUES(@agentUrl)",
                new
                {                    
                    agentUrl = metrics.AgentUrl
                });
            }
        }
        public IList<AgentDto> GetAll()
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.Query<AgentDal>("SELECT * FROM Agents").ToList();
                return _mapper.Map<List<AgentDto>>(result);
            };
        }
    }
}
