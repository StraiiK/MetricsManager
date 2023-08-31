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
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private IMapper _mapper;
        private MetricDbContext _dbContext;

        public RamMetricsRepository(IMapper mapper, MetricDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public IList<RamMetricDto> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            var result = _dbContext.RamMetrics
                .AsNoTracking()
                .Where(metric => metric.Time >= UnixTimeConverter.ToUnixTime(fromTime) && metric.Time <= UnixTimeConverter.ToUnixTime(toTime))
                .OrderByDescending(metric => metric.Id);
            return _mapper.Map<List<RamMetricDto>>(result);
        }

        public IList<RamMetricDto> GetAll()
        {
            var result = _dbContext.RamMetrics.AsNoTracking().OrderByDescending(metric => metric.Id);
            return _mapper.Map<List<RamMetricDto>>(result);
        }

        public void Create(RamMetricDto item)
        {
            _dbContext.RamMetrics.Add(_mapper.Map<RamMetricDal>(item));
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}