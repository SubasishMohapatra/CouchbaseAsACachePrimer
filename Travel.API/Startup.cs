using Couchbase.Extensions.DependencyInjection;
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
using Travel.Domain.Interfaces;
using Travel.Infrastructure;
using Travel.Infrastructure.Extensions;
using Travel.Infrastructure.Interfaces;
using Travel.WebAPI.Service;

namespace Travel.API
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
            services.AddLogging();
            services.AddControllers();
            services.AddScoped<ITravelService, TravelService>();
            services.AddScoped<ITravelRepository, TravelRepository>();
            //services.AddCouchbase(Configuration);
            services.AddCouchbase(options=>options.WithConnectionString("couchbase://localhost")
            .WithCredentials("Administrator","password")
            .WithBuckets("travel-sample"));
            services.AddCouchbaseBucket<ITravelBucketProvider>("travel-sample");
            //services.RegisterCouchbaseNamedBuckets("travel-sample", () => Configuration.GetSection("Couchbase"));            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime)
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
            hostApplicationLifetime.ApplicationStopped.Register(() =>
            {
                app.ApplicationServices.GetRequiredService<ICouchbaseLifetimeService>().Close();
            });
        }
    }
}
