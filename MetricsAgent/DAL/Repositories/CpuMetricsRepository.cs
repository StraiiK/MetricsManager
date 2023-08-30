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
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private IConnectionManager _connectionManager;
        private IMapper _mapper;
        private MetricDbContext _dbContext;

        public CpuMetricsRepository(IConnectionManager connectionManager, IMapper mapper, MetricDbContext dbContext)
        {
            _connectionManager = connectionManager;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public IList<CpuMetricDto> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var result = _dbContext.CpuMetrics
                .AsNoTracking()
                .Where(metric => metric.Time >= UnixTimeConverter.ToUnixTime(fromTime) && metric.Time <= UnixTimeConverter.ToUnixTime(toTime))
                .OrderByDescending(metric => metric.Id);
            return _mapper.Map<List<CpuMetricDto>>(result);
        }

        public IList<CpuMetricDto> GetAll()
        {
            var result = _dbContext.CpuMetrics.AsNoTracking().OrderByDescending(metric => metric.Id);
            return _mapper.Map<List<CpuMetricDto>>(result);
        }

        public void Create(CpuMetricDto item)
        {
            _dbContext.CpuMetrics.Add(_mapper.Map<CpuMetricDal>(item));
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}