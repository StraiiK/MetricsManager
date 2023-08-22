using MetricsAgent.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.SQLite;
using System;
using MetricsAgent.DAL.Models;
using AutoMapper;
using Dapper;
using MetricsAgent.DTO;
using System.Linq;

namespace MetricsAgent.DAL.Repositories
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

        public IList<NetworkMetricDto> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.Query<NetworkMetricDal>("SELECT * FROM NetworkMetrics WHERE time >= @fromTime AND time <= @toTime",
                new
                {
                    fromTime = fromTime.ToUnixTimeMilliseconds(),
                    toTime = toTime.ToUnixTimeMilliseconds()
                }).ToList();
                return _mapper.Map<List<NetworkMetricDto>>(result);
            };
        }

        public IList<NetworkMetricDto> GetAll()
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.Query<NetworkMetricDal>("SELECT * FROM NetworkMetrics").ToList();
                return _mapper.Map<List<NetworkMetricDto>>(result);
            };
        }

        public void Create(NetworkMetricDto item)
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var metrics = _mapper.Map<NetworkMetricDal>(item);
                connection.Execute("INSERT INTO NetworkMetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = metrics.Value,
                    time = metrics.Time
                });
            }
        }
    }
}
