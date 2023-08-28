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
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private IConnectionManager _connectionManager;
        private IMapper _mapper;

        public DotNetMetricsRepository(IConnectionManager connectionManager, IMapper mapper)
        {
            _connectionManager = connectionManager;
            _mapper = mapper;
        }

        public IList<DotNetMetricDto> GetByPeriodFromAgent(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.Query<DotNetMetricDal>("SELECT * FROM DotNetMetrics WHERE AgentId = @agentId " +
                    "AND time >= @fromTime AND time <= @toTime ORDER BY Id DESC",
                new
                {
                    agentId = agentId,
                    fromTime = UnixTimeConverter.ToUnixTime(fromTime),
                    toTime = UnixTimeConverter.ToUnixTime(toTime)
                }).ToList();
                return _mapper.Map<List<DotNetMetricDto>>(result);
            };
        }

        public IList<DotNetMetricDto> GetByPeriodFromAllCluster(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.Query<DotNetMetricDal>("SELECT * FROM DotNetMetrics WHERE time >= @fromTime AND time <= @toTime ORDER BY Id DESC",
                new
                {
                    fromTime = UnixTimeConverter.ToUnixTime(fromTime),
                    toTime = UnixTimeConverter.ToUnixTime(toTime)
                }).ToList();
                return _mapper.Map<List<DotNetMetricDto>>(result);
            };
        }

        public IList<DotNetMetricDto> GetAll()
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.Query<DotNetMetricDal>("SELECT * FROM DotNetMetrics ORDER BY Id DESC").ToList();
                return _mapper.Map<List<DotNetMetricDto>>(result);
            };
        }

        public void Create(DotNetMetricDto item)
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var metrics = _mapper.Map<DotNetMetricDal>(item);
                connection.Execute("INSERT INTO DotNetMetrics(agentId, value, time) VALUES(@agentId, @value, @time)",
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
                var result = connection.QuerySingle<long>("SELECT IFNULL(max(Time), 0) FROM DotNetMetrics WHERE AgentId = @agentId",
                new
                {
                    agentId = agentId
                });
                return UnixTimeConverter.FromUnixTime(result);
            }
        }
    }
}