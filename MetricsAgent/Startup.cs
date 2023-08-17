using MetricsAgent.DAL.InterfaceDal;
using MetricsAgent.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace MetricsAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            ConfigureSqlLiteConnection(services);
            services.AddTransient<IConnectionManager, ConnectionManager>();
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();
            services.AddSingleton<IRomMetricsRepository, RomMetricsRepository>();
        }

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            var connectionmanager = new ConnectionManager();
            using var connection = connectionmanager.CreateOpenedConnection();
            PrepareSchema(connection);
        }

        private void PrepareSchema(SQLiteConnection connection)
        {            
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "DROP TABLE IF EXISTS CpuMetrics";
                command.ExecuteNonQuery();
                command.CommandText = "DROP TABLE IF EXISTS DotNetMetrics";
                command.ExecuteNonQuery();
                command.CommandText = "DROP TABLE IF EXISTS NetworkMetrics";
                command.ExecuteNonQuery();
                command.CommandText = "DROP TABLE IF EXISTS RamMetrics";
                command.ExecuteNonQuery();
                command.CommandText = "DROP TABLE IF EXISTS RomMetrics";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE CpuMetrics(id INTEGER PRIMARY KEY, value INT, time INT)";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE DotNetMetrics(id INTEGER PRIMARY KEY, value INT, time INT)";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE NetworkMetrics(id INTEGER PRIMARY KEY, value INT, time INT)";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE RamMetrics(id INTEGER PRIMARY KEY, value INT, time INT)";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE RomMetrics(id INTEGER PRIMARY KEY, value INT, time INT)";
                command.ExecuteNonQuery();
            }
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
