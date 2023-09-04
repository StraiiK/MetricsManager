using AutoMapper;
using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Repositories
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private IMapper _mapper;
        private MetricsDbContext _dbContext;

        public RamMetricsRepository(IMapper mapper, MetricsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IList<RamMetricDto>> GetByPeriodFromAgentAsync(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.RamMetric
                .AsNoTracking()
                .Where(metric => metric.Time >= UnixTimeConverter.ToUnixTime(fromTime)
                && metric.Time <= UnixTimeConverter.ToUnixTime(toTime)
                && metric.AgentId == agentId)
                .OrderByDescending(metric => metric.AgentId)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<RamMetricDto>>(result);
        }

        public async Task<IList<RamMetricDto>> GetByPeriodFromAllClusterAsync(DateTimeOffset fromTime, DateTimeOffset toTime, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.RamMetric
                .AsNoTracking()
                .Where(metric => metric.Time >= UnixTimeConverter.ToUnixTime(fromTime)
                && metric.Time <= UnixTimeConverter.ToUnixTime(toTime))
                .OrderByDescending(metric => metric.AgentId)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<RamMetricDto>>(result);
        }

        public async Task<IList<RamMetricDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.RamMetric
                .AsNoTracking()
                .OrderByDescending(metric => metric.Id)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<RamMetricDto>>(result);
        }

        public async Task CreateAsync(RamMetricDto item, CancellationToken cancellationToken = default)
        {
            await _dbContext.RamMetric.AddAsync(_mapper.Map<RamMetricDal>(item), cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<DateTimeOffset> GetLastOfTimeAsync(int agentId, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.RamMetric
                .AsNoTracking()
                .Where(metric => metric.AgentId == agentId)
                .Select(metric => (long?)metric.Time)
                .MaxAsync(cancellationToken) ?? 0;
            return UnixTimeConverter.FromUnixTime(result);
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}