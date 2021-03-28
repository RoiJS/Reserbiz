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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ReserbizAPP.LIB.BusinessLogic;
using ReserbizAPP.LIB.DbContexts;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;
using ReserbizAPP.LIB.Helpers;
using System.Security.Claims;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Helpers.Class;

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
            services.AddScoped(typeof(IGeneralInformationRepository<GeneralInformation>), typeof(GeneralInformationRepository));
            services.AddScoped(typeof(IAuthRepository<Account>), typeof(AuthRepository));
            services.AddScoped(typeof(ITenantRepository<Tenant>), typeof(TenantRepository));
            services.AddScoped(typeof(IContactPersonRepository<ContactPerson>), typeof(ContactPersonRepository));
            services.AddScoped(typeof(ISpaceTypeRepository<SpaceType>), typeof(SpaceTypeRepository));
            services.AddScoped(typeof(ISpaceRepository<Space>), typeof(SpaceRepository));
            services.AddScoped(typeof(ITermRepository<Term>), typeof(TermRepository));
            services.AddScoped(typeof(ITermMiscellaneousRepository<TermMiscellaneous>), typeof(TermMiscellaneousRepository));
            services.AddScoped(typeof(IContractRepository<Contract>), typeof(ContractRepository));
            services.AddScoped(typeof(IAccountStatementRepository<AccountStatement>), typeof(AccountStatementRepository));
            services.AddScoped(typeof(IAccountStatementMiscellaneousRepository<AccountStatementMiscellaneous>), typeof(AccountStatementMiscellaneousRepository));
            services.AddScoped(typeof(IClientSettingsRepository<ClientSettings>), typeof(ClientSettingsRepository));
            services.AddScoped(typeof(IPaymentBreakdownRepository<PaymentBreakdown>), typeof(PaymentBreakdownRepository));
            services.AddScoped(typeof(IPenaltyBreakdownRepository<PenaltyBreakdown>), typeof(PenaltyBreakdownRepository));
            services.AddScoped(typeof(IErrorLogRepository<ErrorLog>), typeof(ErrorLogRepository));
            services.AddScoped(typeof(IRefreshTokenRepository<RefreshToken>), typeof(RefreshTokenRepository));
            services.AddScoped(typeof(IDataSeedRepository<IEntity>), typeof(DataSeedRepository));
            services.AddScoped(typeof(IPaginationService), typeof(PaginationService));

            // Register IOptions pattern for AppSettings section
            services.Configure<ApplicationSettings>(Configuration.GetSection("AppSettings"));

            // Register IOptions pattern for SMSAPISettings
            services.Configure<SMSAPISettings>(Configuration.GetSection("SMSAPISettings"));

            // Register IOptions pattern for EmailServerSettings section
            services.Configure<EmailServerSettings>(Configuration.GetSection("EmailServerSettings"));

            // Register Automapper
            services.AddAutoMapper(typeof(Startup).Assembly);

            // Database connection to Reserbiz System Database
            services.AddDbContext<ReserbizDataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("ReserbizDBConnection")));

            // Database connection to any Reserbiz Client Databases
            // Applied dynamic approach if current ef migration is not activated based on appsettings
            var ActivateEFMigration = Convert.ToBoolean(Configuration.GetSection("AppSettings:ActivateEFMigration").Value);

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

                    // If app secret token is not provided, it is always 
                    // assume that the request is going to ReserbizDataContext
                    if (appSecretToken != "")
                    {
                        // Get the client information based on the app secret token
                        var clientInfo = systemDataContext.Clients.FirstOrDefault(c => c.DBHashName == appSecretToken);

                        if (clientInfo == null)
                            throw new Exception("Invalid App secret token. Please make sure that the app secret token you have provided is valid.");

                        // Format and configure connection string for the current http request.
                        var connectionString = String.Format(Configuration.GetConnectionString("ReserbizClientDBTemplateConnection"), clientInfo?.DBName);
                        options.UseSqlServer(connectionString);
                    }

                });
            }

            services.AddMvc().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddCors();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
                            var errorInfo = error.Error;

                            // Application global exception registration.
                            // Whenever the api throws an error, this will catch it and
                            // register to database for production debugging purposes. 
                            using (var serviceScope = app.ApplicationServices.CreateScope())
                            {
                                var services = serviceScope.ServiceProvider;
                                var errorLogRepository = services.GetService<IErrorLogRepository<ErrorLog>>();

                                // Register error details on database
                                await errorLogRepository.RegisterError(
                                    errorInfo.Source,
                                    errorInfo.Message,
                                    errorInfo.StackTrace,
                                    Convert.ToInt32(context.User.Identity.GetUserClaim(ClaimTypes.NameIdentifier))
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

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //app.UseHsts();
            //app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SystemUpdateHub>("api/systemUpdateHub");
            });
        }
    }
}
