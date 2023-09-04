using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricsAgent.DAL
{
    public class MetricDbContext : DbContext
    {
        private readonly IConnectionManager _connectionManager;
        public DbSet<CpuMetricDal> CpuMetrics { get; set; }
        public DbSet<DotNetMetricDal> DotNetMetrics { get; set; }
        public DbSet<NetworkMetricDal> NetworkMetrics { get; set; }
        public DbSet<RamMetricDal> RamMetrics { get; set; }
        public DbSet<RomMetricDal> RomMetrics { get; set; }

        public MetricDbContext(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Здесь мы настраиваем подключение к базе данных (например, к SQL Server)
            optionsBuilder.UseSqlite(_connectionManager.ConnectionString);
        }
    }
}
