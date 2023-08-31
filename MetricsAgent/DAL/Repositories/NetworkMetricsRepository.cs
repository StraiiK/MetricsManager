using AutoMapper;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MetricsAgent.DAL.Repositories
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private IMapper _mapper;
        private MetricDbContext _dbContext;

        public NetworkMetricsRepository(IMapper mapper, MetricDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public IList<NetworkMetricDto> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var result = _dbContext.NetworkMetrics
                .AsNoTracking()
                .Where(metric => metric.Time >= UnixTimeConverter.ToUnixTime(fromTime) && metric.Time <= UnixTimeConverter.ToUnixTime(toTime))
                .OrderByDescending(metric => metric.Id);
            return _mapper.Map<List<NetworkMetricDto>>(result);
        }

        public IList<NetworkMetricDto> GetAll()
        {
            var result = _dbContext.NetworkMetrics.AsNoTracking().OrderByDescending(metric => metric.Id);
            return _mapper.Map<List<NetworkMetricDto>>(result);
        }

        public void Create(NetworkMetricDto item)
        {
            _dbContext.NetworkMetrics.Add(_mapper.Map<NetworkMetricDal>(item));
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}