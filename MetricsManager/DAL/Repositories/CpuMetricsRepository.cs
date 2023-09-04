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
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private IMapper _mapper;
        private MetricsDbContext _dbContext;

        public CpuMetricsRepository(IMapper mapper, MetricsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IList<CpuMetricDto>> GetByPeriodFromAgentAsync(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.CpuMetric
                .AsNoTracking()
                .Where(metric => metric.Time >= UnixTimeConverter.ToUnixTime(fromTime) 
                && metric.Time <= UnixTimeConverter.ToUnixTime(toTime) 
                && metric.AgentId == agentId)
                .OrderByDescending(metric => metric.AgentId)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<CpuMetricDto>>(result);
        }

        public async Task<IList<CpuMetricDto>> GetByPeriodFromAllClusterAsync(DateTimeOffset fromTime, DateTimeOffset toTime, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.CpuMetric
                .AsNoTracking()
                .Where(metric => metric.Time >= UnixTimeConverter.ToUnixTime(fromTime)
                && metric.Time <= UnixTimeConverter.ToUnixTime(toTime))
                .OrderByDescending(metric => metric.AgentId)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<CpuMetricDto>>(result);
        }

        public async Task <IList<CpuMetricDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.CpuMetric
                .AsNoTracking()
                .OrderByDescending(metric => metric.Id)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<CpuMetricDto>>(result);
        }

        public async Task CreateAsync(CpuMetricDto item, CancellationToken cancellationToken = default)
        {
            await _dbContext.CpuMetric.AddAsync(_mapper.Map<CpuMetricDal>(item), cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task <DateTimeOffset> GetLastOfTimeAsync(int agentId, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.CpuMetric
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