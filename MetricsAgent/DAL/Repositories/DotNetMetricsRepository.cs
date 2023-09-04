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
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private IMapper _mapper;
        private MetricDbContext _dbContext;

        public DotNetMetricsRepository(IMapper mapper, MetricDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IList<DotNetMetricDto>> GetByTimePeriodAsync(DateTimeOffset fromTime, DateTimeOffset toTime, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.DotNetMetrics
                .AsNoTracking()
                .Where(metric => metric.Time >= UnixTimeConverter.ToUnixTime(fromTime) && metric.Time <= UnixTimeConverter.ToUnixTime(toTime))
                .OrderByDescending(metric => metric.Id).ToListAsync();
            return _mapper.Map<IList<DotNetMetricDto>>(result);
        }

        public async Task<IList<DotNetMetricDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.DotNetMetrics.AsNoTracking().OrderByDescending(metric => metric.Id).ToListAsync();
            return _mapper.Map<IList<DotNetMetricDto>>(result);
        }

        public async Task CreateAsync(DotNetMetricDto item, CancellationToken cancellationToken = default)
        {
            await _dbContext.DotNetMetrics.AddAsync(_mapper.Map<DotNetMetricDal>(item), cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}