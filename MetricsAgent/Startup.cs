using MetricsAgent.DAL.Interfaces;
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
using MetricsAgent.DAL.Repositories;
using MetricsAgent.DAL;
using AutoMapper;
using FluentMigrator.Runner;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using Microsoft.OpenApi.Models;
using MetricsAgent.Schedule;
using MetricsAgent.Schedule.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;

namespace MetricsAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            var connectionManager = new ConnectionManager();

            using (var context = new MetricDbContext(connectionManager)) 
            {
                context.Database.EnsureCreated(); // Создание базы данных, если она ещё не существует
                context.SaveChanges();
            }

            services.AddControllers();

            services.AddDbContext<MetricDbContext>();
            services.AddTransient<MetricDbContext>();

            services.AddSingleton(mapper);
            services.AddSingleton<IConnectionManager>(connectionManager);
            services.AddTransient<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddTransient<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddTransient<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddTransient<IRamMetricsRepository, RamMetricsRepository>();
            services.AddTransient<IRomMetricsRepository, RomMetricsRepository>();

            services.AddHostedService<QuartzHostedService>();
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton<DotNetMetricJob>();
            services.AddSingleton<NetworkMetricJob>();
            services.AddSingleton<RamMetricJob>();
            services.AddSingleton<RomMetricJob>();
            services.AddSingleton(new JobSchedule(jobType: typeof(CpuMetricJob), cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(jobType: typeof(DotNetMetricJob), cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(jobType: typeof(NetworkMetricJob), cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(jobType: typeof(RamMetricJob), cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(jobType: typeof(RomMetricJob), cronExpression: "0/5 * * * * ?"));

            //services.AddFluentMigratorCore()
            //    .ConfigureRunner(rb => rb.AddSQLite()
            //    .WithGlobalConnectionString(connectionManager.ConnectionString)
            //    .ScanIn(typeof(Startup).Assembly).For.Migrations()
            //    ).AddLogging(lb => lb
            //    .AddFluentMigratorConsole());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API сервиса агента сбора метрик",
                    Description = "Здесь можно поиграть с api нашего сервиса",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Straik",
                        Email = string.Empty,
                        Url = new Uri("https://kremlin.ru"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Можно указать, под какой лицензией всё опубликовано",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });
        }
                
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //runner.MigrateUp();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API сервиса агента сбора метрик");
            });

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
