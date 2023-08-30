using MetricsAgent.DTO;
using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL
{
    public class MetricDbContext : DbContext
    {
        private readonly IConnectionManager _connectionManager;
        public DbSet<CpuMetricDal> CpuMetrics { get; set; }
        public DbSet<DotNetMetricDal> DotNetMetrics { get; set; }

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
