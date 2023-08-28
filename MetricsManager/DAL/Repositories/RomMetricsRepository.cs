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
    public class RomMetricsRepository : IRomMetricsRepository
    {
        private IConnectionManager _connectionManager;
        private IMapper _mapper;

        public RomMetricsRepository(IConnectionManager connectionManager, IMapper mapper)
        {
            _connectionManager = connectionManager;
            _mapper = mapper;
        }

        public IList<RomMetricDto> GetByPeriodFromAgent(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.Query<RomMetricDal>("SELECT * FROM RomMetrics WHERE AgentId = @agentId " +
                    "AND time >= @fromTime AND time <= @toTime ORDER BY Id DESC",
                new
                {
                    agentId = agentId,
                    fromTime = UnixTimeConverter.ToUnixTime(fromTime),
                    toTime = UnixTimeConverter.ToUnixTime(toTime)
                }).ToList();
                return _mapper.Map<List<RomMetricDto>>(result);
            };
        }

        public IList<RomMetricDto> GetByPeriodFromAllCluster(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.Query<RomMetricDal>("SELECT * FROM RomMetrics WHERE time >= @fromTime AND time <= @toTime ORDER BY Id DESC",
                new
                {
                    fromTime = UnixTimeConverter.ToUnixTime(fromTime),
                    toTime = UnixTimeConverter.ToUnixTime(toTime)
                }).ToList();
                return _mapper.Map<List<RomMetricDto>>(result);
            };
        }

        public IList<RomMetricDto> GetAll()
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.Query<RomMetricDal>("SELECT * FROM RomMetrics ORDER BY Id DESC").ToList();
                return _mapper.Map<List<RomMetricDto>>(result);
            };
        }

        public void Create(RomMetricDto item)
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var metrics = _mapper.Map<RomMetricDal>(item);
                connection.Execute("INSERT INTO RomMetrics(agentId, value, time) VALUES(@agentId, @value, @time)",
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
                var result = connection.QuerySingle<long>("SELECT IFNULL(max(Time), 0) FROM RomMetrics WHERE AgentId = @agentId",
                new
                {
                    agentId = agentId
                });
                return UnixTimeConverter.FromUnixTime(result);
            }
        }
    }
}