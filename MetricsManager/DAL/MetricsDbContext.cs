using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricsManager.DAL
{
    public class MetricsDbContext : DbContext
    {
        private readonly IConnectionManager _connectionManager;
        public DbSet<AgentDal> Agent { get; set; }
        public DbSet<CpuMetricDal> CpuMetric { get; set; }
        public DbSet<DotNetMetricDal> DotNetMetric { get; set; }
        public DbSet<NetworkMetricDal> NetworkMetric { get; set; }
        public DbSet<RamMetricDal> RamMetric { get; set; }
        public DbSet<RomMetricDal> RomMetric { get; set; }


        public MetricsDbContext(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionManager.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgentDal>().HasKey(agent => agent.AgentId);
        }
    }
}
