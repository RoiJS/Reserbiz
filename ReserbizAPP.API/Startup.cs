using System;
using System.Linq;
using System.Net;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ReserbizAPP.LIB.BusinessLogic;
using ReserbizAPP.LIB.DbContexts;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.API
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

            // Register application repository classes for dependency injection
            services.AddScoped<IDataContextHelper, DataContextHelper>();
            services.AddScoped(typeof(IReserbizRepository<>), typeof(ReserbizRepository<>));
            services.AddScoped(typeof(IClientRepository<Client>), typeof(ClientRepository));
            services.AddScoped(typeof(IAuthRepository<Account>), typeof(AuthRepository));
            services.AddScoped(typeof(ITenantRepository<Tenant>), typeof(TenantRepository));
            services.AddScoped(typeof(IContactPersonRepository<ContactPerson>), typeof(ContactPersonRepository));
            services.AddScoped(typeof(ISpaceTypeRepository<SpaceType>), typeof(SpaceTypeRepository));
            services.AddAutoMapper(typeof(Startup).Assembly);

            // Database connection to Reserbiz System Database
            services.AddDbContext<ReserbizDataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("ReserbizDBConnection")));

            // Database connection to any Reserbiz Client Databases
            // Applied dynamic approach if current ef migration is not activated based on appsettings
            var ActivateEFMigration = Configuration.GetValue<bool>("ActivateEFMigration");
            if (ActivateEFMigration)
            {
                services.AddDbContext<ReserbizClientDataContext>(
                    x => x.UseSqlServer(Configuration.GetConnectionString("ReserbizClientDeveloperDBConnection"))
                );
            }
            else
            {
                // This section performs dynamic setting up of connection string for each http request.
                // Getting the database name based on the App-Secret-Token header which is the encrypted version
                // of the database name.
                services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
                services.AddDbContext<ReserbizClientDataContext>((serviceProvider, options) =>
                {
                    var httpContext = serviceProvider.GetService<IHttpContextAccessor>().HttpContext;
                    var systemDataContext = serviceProvider.GetService<ReserbizDataContext>();

                    // Get the encrypted App-Secret-Token header
                    var appSecretToken = httpContext.Request.Headers["App-Secret-Token"].ToString();

                    // Get the client information based on the app secret token
                    var clientInfo = systemDataContext.Clients.FirstOrDefault(c => c.DBHashName == appSecretToken);

                    // Formatt and configure connection string for the current http request.
                    var connectionString = String.Format(Configuration.GetConnectionString("ReserbizClientDBTemplateConnection"), clientInfo?.DBName);
                    options.UseSqlServer(connectionString);
                });
            }

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddCors();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
                            context.Response.Headers.Add("Application-Error", error.Error.Message);
                            context.Response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
                            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //app.UseHsts();
            //app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
