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
    public class RomMetricsRepository : IRomMetricsRepository
    {
        private IConnectionManager _connectionManager;
        private IMapper _mapper;
        private MetricDbContext _dbContext;

        public RomMetricsRepository(IConnectionManager connectionManager, IMapper mapper, MetricDbContext dbContext)
        {
            _connectionManager = connectionManager;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public IList<RomMetricDto> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var result = _dbContext.RomMetrics
                .AsNoTracking()
                .Where(metric => metric.Time >= UnixTimeConverter.ToUnixTime(fromTime) && metric.Time <= UnixTimeConverter.ToUnixTime(toTime))
                .OrderByDescending(metric => metric.Id);
            return _mapper.Map<List<RomMetricDto>>(result);
        }

        public IList<RomMetricDto> GetAll()
        {
            var result = _dbContext.RomMetrics.AsNoTracking().OrderByDescending(metric => metric.Id);
            return _mapper.Map<List<RomMetricDto>>(result);
        }

        public void Create(RomMetricDto item)
        {
            _dbContext.RomMetrics.Add(_mapper.Map<RomMetricDal>(item));
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}