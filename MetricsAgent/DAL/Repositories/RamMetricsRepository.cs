using AutoMapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task<IList<RamMetricDto>> GetByTimePeriodAsync(DateTimeOffset fromTime, DateTimeOffset toTime, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.RamMetrics
                .AsNoTracking()
                .Where(metric => metric.Time >= UnixTimeConverter.ToUnixTime(fromTime) && metric.Time <= UnixTimeConverter.ToUnixTime(toTime))
                .OrderByDescending(metric => metric.Id).ToListAsync();
            return _mapper.Map<IList<RamMetricDto>>(result);
        }

        public async Task<IList<RamMetricDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.RamMetrics.AsNoTracking().OrderByDescending(metric => metric.Id).ToListAsync();
            return _mapper.Map<IList<RamMetricDto>>(result);
        }

        public async Task CreateAsync(RamMetricDto item, CancellationToken cancellationToken = default)
        {
            await _dbContext.RamMetrics.AddAsync(_mapper.Map<RamMetricDal>(item), cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}