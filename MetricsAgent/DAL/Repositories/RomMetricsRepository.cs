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
    public class RomMetricsRepository : IRomMetricsRepository
    {
        private IMapper _mapper;
        private MetricDbContext _dbContext;

        public RomMetricsRepository(IMapper mapper, MetricDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IList<RomMetricDto>> GetByTimePeriodAsync(DateTimeOffset fromTime, DateTimeOffset toTime, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.RomMetrics
                .AsNoTracking()
                .Where(metric => metric.Time >= UnixTimeConverter.ToUnixTime(fromTime) && metric.Time <= UnixTimeConverter.ToUnixTime(toTime))
                .OrderByDescending(metric => metric.Id).ToListAsync();
            return _mapper.Map<IList<RomMetricDto>>(result);
        }

        public async Task<IList<RomMetricDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.RomMetrics.AsNoTracking().OrderByDescending(metric => metric.Id).ToListAsync();
            return _mapper.Map<IList<RomMetricDto>>(result);
        }

        public async Task CreateAsync(RomMetricDto item, CancellationToken cancellationToken = default)
        {
            await _dbContext.RomMetrics.AddAsync(_mapper.Map<RomMetricDal>(item), cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}