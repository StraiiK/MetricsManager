using FluentMigrator.Runner;
using MetricsManager.DAL.Interfaces;
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
using MetricsManager.DAL;
using AutoMapper;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using MetricsManager.DAL.Repositories;
using MetricsManager.Client;
using System.Text.Json;
using Polly;
using Microsoft.OpenApi.Models;
using MetricsManager.Schedule.Jobs;
using MetricsManager.Schedule;
using Microsoft.EntityFrameworkCore;

namespace MetricsManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public async void ConfigureServices(IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            var connectionManager = new ConnectionManager();

            using (var context = new MetricsDbContext(connectionManager))
            {
                await context.Database.MigrateAsync();
                await context.SaveChangesAsync();
            }

            services.AddDbContext<MetricsDbContext>();
            services.AddTransient<MetricsDbContext>();

            services.AddSwaggerGen();
            services.AddControllers();
            services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>().AddTransientHttpErrorPolicy(p =>
                p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));

            services.AddSingleton(mapper);
            services.AddSingleton<IConnectionManager>(connectionManager);
            services.AddTransient<IAgentRepository, AgentRepository>();
            services.AddTransient<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddTransient<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddTransient<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddTransient<IRamMetricsRepository, RamMetricsRepository>();
            services.AddTransient<IRomMetricsRepository, RomMetricsRepository>();

            services.AddHostedService<QuartzHostedService>();
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddTransient<CpuMetricJob>();
            services.AddTransient<DotNetMetricJob>();
            services.AddTransient<NetworkMetricJob>();
            services.AddTransient<RamMetricJob>();
            services.AddTransient<RomMetricJob>();
            services.AddSingleton(new JobSchedule(jobType: typeof(CpuMetricJob), cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(jobType: typeof(DotNetMetricJob), cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(jobType: typeof(NetworkMetricJob), cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(jobType: typeof(RamMetricJob), cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(jobType: typeof(RomMetricJob), cronExpression: "0/5 * * * * ?"));

            services.AddSingleton<JsonSerializerOptions>(provider => new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API ������� ������ ����� ������",
                    Description = "����� ����� �������� � api ������ �������",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Straik",
                        Email = string.Empty,
                        Url = new Uri("https://kremlin.ru"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "����� �������, ��� ����� ��������� �� ������������",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API ������� ������ ����� ������");
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
