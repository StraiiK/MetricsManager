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
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private IConnectionManager _connectionManager;
        private IMapper _mapper;
        private MetricDbContext _dbContext;

        public DotNetMetricsRepository(IConnectionManager connectionManager, IMapper mapper, MetricDbContext dbContext)
        {
            _connectionManager = connectionManager;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public IList<DotNetMetricDto> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var result = _dbContext.DotNetMetrics
                .AsNoTracking()
                .Where(metric => metric.Time >= UnixTimeConverter.ToUnixTime(fromTime) && metric.Time <= UnixTimeConverter.ToUnixTime(toTime))
                .OrderByDescending(metric => metric.Id);
            return _mapper.Map<List<DotNetMetricDto>>(result);
        }

        public IList<DotNetMetricDto> GetAll()
        {
            var result = _dbContext.DotNetMetrics.AsNoTracking().OrderByDescending(metric => metric.Id);
            return _mapper.Map<List<DotNetMetricDto>>(result);
        }

        public void Create(DotNetMetricDto item)
        {
            _dbContext.DotNetMetrics.Add(_mapper.Map<DotNetMetricDal>(item));
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}