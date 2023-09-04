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
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private IMapper _mapper;
        private MetricsDbContext _dbContext;

        public DotNetMetricsRepository(IMapper mapper, MetricsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IList<DotNetMetricDto>> GetByPeriodFromAgentAsync(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.DotNetMetric
                .AsNoTracking()
                .Where(metric => metric.Time >= UnixTimeConverter.ToUnixTime(fromTime)
                && metric.Time <= UnixTimeConverter.ToUnixTime(toTime)
                && metric.AgentId == agentId)
                .OrderByDescending(metric => metric.AgentId)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<DotNetMetricDto>>(result);
        }

        public async Task<IList<DotNetMetricDto>> GetByPeriodFromAllClusterAsync(DateTimeOffset fromTime, DateTimeOffset toTime, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.DotNetMetric
                .AsNoTracking()
                .Where(metric => metric.Time >= UnixTimeConverter.ToUnixTime(fromTime)
                && metric.Time <= UnixTimeConverter.ToUnixTime(toTime))
                .OrderByDescending(metric => metric.AgentId)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<DotNetMetricDto>>(result);
        }

        public async Task<IList<DotNetMetricDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.DotNetMetric
                .AsNoTracking()
                .OrderByDescending(metric => metric.Id)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<DotNetMetricDto>>(result);
        }

        public async Task CreateAsync(DotNetMetricDto item, CancellationToken cancellationToken = default)
        {
            await _dbContext.DotNetMetric.AddAsync(_mapper.Map<DotNetMetricDal>(item), cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<DateTimeOffset> GetLastOfTimeAsync(int agentId, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.DotNetMetric
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