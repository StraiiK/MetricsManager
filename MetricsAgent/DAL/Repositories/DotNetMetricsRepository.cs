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
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private IConnectionManager _connectionManager;
        private IMapper _mapper;

        public DotNetMetricsRepository(IConnectionManager connectionManager, IMapper mapper)
        {
            _connectionManager = connectionManager;
            _mapper = mapper;
        }

        public IList<DotNetMetricDto> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.Query<DotNetMetricDal>("SELECT * FROM DotNetMetrics WHERE time >= @fromTime AND time <= @toTime",
                new
                {
                    fromTime = fromTime.ToUnixTimeMilliseconds(),
                    toTime = toTime.ToUnixTimeMilliseconds()
                }).ToList();
                return _mapper.Map<List<DotNetMetricDto>>(result);
            };
        }

        public IList<DotNetMetricDto> GetAll()
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var result = connection.Query<DotNetMetricDal>("SELECT * FROM DotNetMetrics").ToList();
                return _mapper.Map<List<DotNetMetricDto>>(result);
            };
        }

        public void Create(DotNetMetricDto item)
        {
            using (var connection = _connectionManager.CreateOpenedConnection())
            {
                var metrics = _mapper.Map<DotNetMetricDal>(item);
                connection.Execute("INSERT INTO DotNetMetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = metrics.Value,
                    time = metrics.Time
                });
            }
        }
    }
}
