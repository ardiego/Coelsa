using Coelsa.Common;
using Coelsa.Models;
using Coelsa.Repositories;
using Coelsa.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Coelsa.WebApi
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
            services.AddControllers()
            .AddJsonOptions(options => {
                options.JsonSerializerOptions.IgnoreNullValues = true;  //ignore values in null
            });

            //Configuration
            services.Configure<SettingModel>(Configuration.GetSection("Settings"));

            //Logger
            services.AddSingleton<NLogger>();

            // Repositor
            services.AddScoped<IContactRepository, ContactRepository>();

            // Services
            services.AddTransient<IContactService, ContactService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
