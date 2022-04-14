using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Project
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<ProjectHttpApiHostModule>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var sqlStorage = new SqlServerStorage("Data Source=DESKTOP-95249B7\\SQLEXPRESS;Initial Catalog=Project;Integrated Security=True");
            var options = new BackgroundJobServerOptions
            {
                ServerName = "Test Server"
            };
            JobStorage.Current = sqlStorage;
            app.InitializeApplication();
        }
    }
}
