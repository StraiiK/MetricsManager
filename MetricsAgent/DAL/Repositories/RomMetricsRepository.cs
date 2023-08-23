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
    public class RomMetricsRepository : IRomMetricsRepository
    {
        private IConnectionManager _connectionManager;
        private IMapper _mapper;

        public RomMetricsRepository(IConnectionManager connectionManager, IMapper mapper)
        {
            _connectionManager = connectionManager;
            _mapper = mapper;
        }

        public IList<RomMetricDto> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
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
                connection.Execute("INSERT INTO RomMetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = metrics.Value,
                    time = metrics.Time
                });
            }
        }
    }
}
