using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReserbizAPP.API;
using ReserbizAPP.LIB.Helpers.Class;

namespace ReserbizAPP.IntegrationTests
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>
    {

        public IConfiguration Configuration { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Console.WriteLine(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            builder.ConfigureAppConfiguration(config =>
            {
                Configuration = new ConfigurationBuilder()
                    .AddJsonFile("integrationSettings.json")
                    .Build();

                config.AddConfiguration(Configuration);
            });

            builder.ConfigureTestServices(services =>
            {
                services.Configure<DefaultAccountCredentials>(Configuration.GetSection("DefaultAccountCredentials"));
                services.Configure<DefaultUserAccountDetails>(Configuration.GetSection("DefaultUserAccountDetails"));
            });
        }
    }
}
