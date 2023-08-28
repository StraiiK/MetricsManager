using AutoMapper;
using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MetricsManager.DAL.Repositories
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private IConnectionManager _connectionManager;
        private IMapper _mapper;

        public NetworkMetricsRepository(IConnectionManager connectionManager, IMapper mapper)
        {
            _connectionManager = connectionManager;
            _mapper = mapper;
        }

        public IList<NetworkMetricDto> GetByPeriodFromAgent(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.Query<NetworkMetricDal>("SELECT * FROM NetworkMetrics WHERE AgentId = @agentId " +
                    "AND time >= @fromTime AND time <= @toTime ORDER BY Id DESC",
                new
                {
                    agentId = agentId,
                    fromTime = UnixTimeConverter.ToUnixTime(fromTime),
                    toTime = UnixTimeConverter.ToUnixTime(toTime)
                }).ToList();
                return _mapper.Map<List<NetworkMetricDto>>(result);
            };
        }

        public IList<NetworkMetricDto> GetByPeriodFromAllCluster(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.Query<NetworkMetricDal>("SELECT * FROM NetworkMetrics WHERE time >= @fromTime AND time <= @toTime ORDER BY Id DESC",
                new
                {
                    fromTime = UnixTimeConverter.ToUnixTime(fromTime),
                    toTime = UnixTimeConverter.ToUnixTime(toTime)
                }).ToList();
                return _mapper.Map<List<NetworkMetricDto>>(result);
            };
        }

        public IList<NetworkMetricDto> GetAll()
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.Query<NetworkMetricDal>("SELECT * FROM NetworkMetrics ORDER BY Id DESC").ToList();
                return _mapper.Map<List<NetworkMetricDto>>(result);
            };
        }

        public void Create(NetworkMetricDto item)
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var metrics = _mapper.Map<NetworkMetricDal>(item);
                connection.Execute("INSERT INTO NetworkMetrics(agentId, value, time) VALUES(@agentId, @value, @time)",
                new
                {
                    agentId = item.AgentId,
                    value = metrics.Value,
                    time = metrics.Time
                });
            }
        }

        public DateTimeOffset GetLastOfTime(int agentId)
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.QuerySingle<long>("SELECT IFNULL(max(Time), 0) FROM NetworkMetrics WHERE AgentId = @agentId",
                new
                {
                    agentId = agentId
                });
                return UnixTimeConverter.FromUnixTime(result);
            }
        }
    }
}