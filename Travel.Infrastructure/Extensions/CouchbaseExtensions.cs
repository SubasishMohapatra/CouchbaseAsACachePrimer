using Couchbase.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using Travel.Infrastructure.Interfaces;

namespace Travel.Infrastructure.Extensions
{
    public static class CouchbaseExtensions
    {
        public static IServiceCollection RegisterCouchbaseNamedBuckets(this IServiceCollection services, string bucketName, Func<IConfiguration> getCouchbaseConfigSection)
        {
            services
    //.AddCouchbase(getCouchbaseConfigSection())
    .AddCouchbaseBucket<ITravelBucketProvider>(bucketName);
            return services;
        }
    }
}
