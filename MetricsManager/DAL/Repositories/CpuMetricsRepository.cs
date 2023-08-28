using AutoMapper;
using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MetricsManager.DAL.Repositories
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private IConnectionManager _connectionManager;
        private IMapper _mapper;

        public CpuMetricsRepository(IConnectionManager connectionManager, IMapper mapper)
        {
            _connectionManager = connectionManager;
            _mapper = mapper;
        }

        public IList<CpuMetricDto> GetByPeriodFromAgent(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.Query<CpuMetricDal>("SELECT * FROM CpuMetrics WHERE AgentId = @agentId " +
                    "AND time >= @fromTime AND time <= @toTime ORDER BY Id DESC",
                new
                {
                    agentId = agentId,
                    fromTime = UnixTimeConverter.ToUnixTime(fromTime),
                    toTime = UnixTimeConverter.ToUnixTime(toTime)
                }).ToList();
                return _mapper.Map<List<CpuMetricDto>>(result);
            };
        }

        public IList<CpuMetricDto> GetByPeriodFromAllCluster(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.Query<CpuMetricDal>("SELECT * FROM CpuMetrics WHERE time >= @fromTime AND time <= @toTime ORDER BY Id DESC",
                new
                {
                    fromTime = UnixTimeConverter.ToUnixTime(fromTime),
                    toTime = UnixTimeConverter.ToUnixTime(toTime)
                }).ToList();
                return _mapper.Map<List<CpuMetricDto>>(result);
            };
        }

        public IList<CpuMetricDto> GetAll()
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.Query<CpuMetricDal>("SELECT * FROM CpuMetrics ORDER BY Id DESC").ToList();
                return _mapper.Map<List<CpuMetricDto>>(result);
            };
        }

        public void Create(CpuMetricDto item)
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var metrics = _mapper.Map<CpuMetricDal>(item);
                connection.Execute("INSERT INTO CpuMetrics(agentId, value, time) VALUES(@agentId, @value, @time)",
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
            using(var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.QuerySingle<long>("SELECT IFNULL(max(Time), 0) FROM CpuMetrics WHERE AgentId = @agentId",
                new
                {
                    agentId = agentId
                });
                return UnixTimeConverter.FromUnixTime(result); 
            }
        }
    }
}