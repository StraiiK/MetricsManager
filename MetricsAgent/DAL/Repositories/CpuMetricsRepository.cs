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
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private IMapper _mapper;
        private MetricDbContext _dbContext;

        public CpuMetricsRepository(IMapper mapper, MetricDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IList<CpuMetricDto>> GetByTimePeriodAsync(DateTimeOffset fromTime, DateTimeOffset toTime, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.CpuMetrics
                .AsNoTracking()
                .Where(metric => metric.Time >= UnixTimeConverter.ToUnixTime(fromTime) && metric.Time <= UnixTimeConverter.ToUnixTime(toTime))
                .OrderByDescending(metric => metric.Id).ToListAsync();
            return _mapper.Map<IList<CpuMetricDto>>(result);
        }

        public async Task<IList<CpuMetricDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.CpuMetrics
                .AsNoTracking()
                .OrderByDescending(metric => metric.Id)
                .ToListAsync();
            return _mapper.Map<IList<CpuMetricDto>>(result);
        }

        public async Task CreateAsync(CpuMetricDto item, CancellationToken cancellationToken = default)
        {
            await _dbContext.CpuMetrics.AddAsync(_mapper.Map<CpuMetricDal>(item), cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}