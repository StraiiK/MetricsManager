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
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private IMapper _mapper;
        private MetricDbContext _dbContext;

        public NetworkMetricsRepository(IMapper mapper, MetricDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IList<NetworkMetricDto>> GetByTimePeriodAsync(DateTimeOffset fromTime, DateTimeOffset toTime, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.NetworkMetrics
                .AsNoTracking()
                .Where(metric => metric.Time >= UnixTimeConverter.ToUnixTime(fromTime) && metric.Time <= UnixTimeConverter.ToUnixTime(toTime))
                .OrderByDescending(metric => metric.Id).ToListAsync();
            return _mapper.Map<IList<NetworkMetricDto>>(result);
        }

        public async Task<IList<NetworkMetricDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.NetworkMetrics.AsNoTracking().OrderByDescending(metric => metric.Id).ToListAsync();
            return _mapper.Map<IList<NetworkMetricDto>>(result);
        }

        public async Task CreateAsync(NetworkMetricDto item, CancellationToken cancellationToken = default)
        {
            await _dbContext.NetworkMetrics.AddAsync(_mapper.Map<NetworkMetricDal>(item), cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}