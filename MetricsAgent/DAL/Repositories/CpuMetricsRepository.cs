using AutoMapper;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MetricsAgent.DAL.Repositories
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

        public IList<CpuMetricDto> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
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
                connection.Execute("INSERT INTO CpuMetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = metrics.Value,
                    time = metrics.Time
                });
            }
        }
    }
}


