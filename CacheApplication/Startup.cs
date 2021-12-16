using Couchbase;
using Couchbase.Extensions.Caching;
using Couchbase.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace CacheApplication
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
            services.AddLogging();

            services.AddCouchbase(options =>
            options
            //.WithConnectionString("couchbase://127.0.0.1")
            .WithConnectionString("couchbase://localhost")
            //.WithConnectionString("couchbase://172.17.0.2")  //Docker localhost multinode
            //.WithConnectionString("couchbase://10.2.32.175") //Docker ACSRMXDCNINT02 singlenode
            //.WithConnectionString("couchbase://10.2.32.175:11210")
            .WithCredentials("Administrator", "password")
            .WithBuckets("Cache-Sample"));
            services.AddCouchbaseBucket<ICouchbaseCacheBucketProvider>("Cache-Sample");
            //services.RegisterCouchbaseNamedBuckets("travel-sample", () => Configuration.GetSection("Couchbase"));
            services.AddDistributedCouchbaseCache("Cache-Sample", opt => { });

  //          var options = new ClusterOptions()
  //.WithConnectionString("couchbase://localhost")
  //.WithCredentials(username: "Administrator", password: "password")
  //.WithBuckets("Cache-Sample");
            //.WithLogging(LoggerFactory.Create(builder =>
            //{
            //    builder.AddFilter("Couchbase", LogLevel.Debug)
            //    .AddEventLog();
            //}));                

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
