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
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private IConnectionManager _connectionManager;
        private IMapper _mapper;

        public RamMetricsRepository(IConnectionManager connectionManager, IMapper mapper)
        {
            _connectionManager = connectionManager;
            _mapper = mapper;
        }

        public IList<RamMetricDto> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.Query<RamMetricDal>("SELECT * FROM RamMetrics WHERE time >= @fromTime AND time <= @toTime",
                new
                {
                    fromTime = fromTime.ToUnixTimeMilliseconds(),
                    toTime = toTime.ToUnixTimeMilliseconds()
                }).ToList();
                return _mapper.Map<List<RamMetricDto>>(result);
            };
        }

        public IList<RamMetricDto> GetAll()
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.Query<RamMetricDal>("SELECT * FROM RamMetrics").ToList();
                return _mapper.Map<List<RamMetricDto>>(result);
            };
        }

        public void Create(RamMetricDto item)
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var metrics = _mapper.Map<RamMetricDal>(item);
                connection.Execute("INSERT INTO RamMetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = metrics.Value,
                    time = metrics.Time
                });
            }
        }
    }
}
