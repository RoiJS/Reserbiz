using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.DbContexts;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.Hangfire.Helpers.Services;
using ReserbizAPP.Hangfire.Interfaces;
using ReserbizAPP.LIB.Models;
using ReserbizAPP.LIB.BusinessLogic;
using ReserbizAPP.Hangfire.Helpers.Extensions;
using ReserbizAPP.LIB.Helpers.Class;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using ReserbizAPP.Hangfire.Helpers.Filters;

namespace ReserbizAPP.Hangfire
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                        .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register application repository classes for dependency injection
            services.AddScoped<IDataContextHelper, DataContextHelper>();
            services.AddScoped(typeof(IReserbizRepository<>), typeof(ReserbizRepository<>));
            services.AddScoped(typeof(IClientRepository<Client>), typeof(ClientRepository));
            services.AddScoped(typeof(IGlobalErrorLogRepository<GlobalErrorLog>), typeof(GlobalErrorLogRepository));
            services.AddScoped(typeof(IReserbizRecurringJobsService), typeof(ReserbizRecurringJobsService));

            // Register IOptions pattern for AppSettings section
            services.Configure<ReserbizHangFireApplicationSettings>(Configuration.GetSection("AppSettings"));

            // Register IOptions pattern for EmailServerSettings section
            services.Configure<EmailServerSettings>(Configuration.GetSection("EmailServerSettings"));

            // Database connection to Reserbiz System Database
            services.AddDbContext<ReserbizDataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("ReserbizDBConnection")));

            // Database connection to any Reserbiz Client Databases
            services.AddDbContext<ReserbizClientDataContext>(
                                x => x.UseSqlServer(Configuration.GetConnectionString("ReserbizClientDeveloperDBConnection"))
                            );

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("ReserbizDBConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReserbizAPP.Hangfire", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReserbizAPP.Hangfire v1"));
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            var errorInfo = error.Error;

                            // Application global exception registration.
                            // Whenever the api throws an error, this will catch it and
                            // register to dbReserbizSYSSQL database for production debugging purposes. 
                            using (var serviceScope = app.ApplicationServices.CreateScope())
                            {
                                var services = serviceScope.ServiceProvider;
                                var globalErrorLogRepository = services.GetService<IGlobalErrorLogRepository<GlobalErrorLog>>();

                                // Register error details on database
                                await globalErrorLogRepository.RegisterGlobalError(
                                    errorInfo.Source,
                                    errorInfo.Message,
                                    errorInfo.StackTrace,
                                    1
                                );
                            }

                            // This will attach the error information along with the http response
                            context.Response.Headers.Add("Application-Error", errorInfo.Message);
                            context.Response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
                            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                            await context.Response.WriteAsync(errorInfo.Message);
                        }
                    });
                });
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Add hangfire built-in dashboard
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });

            // Register hangfire recurring jobs here for Reserbiz
            app.RegisterReserbizRecurringJobs();
        }
    }
}
