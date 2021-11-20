using Couchbase.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel.Domain;
using Travel.Domain.Interfaces;
using Travel.Infrastructure.Interfaces;

namespace Travel.Infrastructure
{
    public class TravelRepository:ITravelRepository
    {
        private readonly ITravelBucketProvider _travelBucketProvider;
        private readonly IClusterProvider _clusterProvider;

        public TravelRepository(ITravelBucketProvider travelBucketProvider, IClusterProvider clusterProvider)
        {
            _travelBucketProvider = travelBucketProvider;
            _clusterProvider = clusterProvider;
        }
        public async Task<IEnumerable<Airline>> GetAirlines()
        {
            try
            {
                var travelBucket = await _travelBucketProvider.GetBucketAsync();//.ConfigureAwait(false);
                var collection = travelBucket.DefaultCollection();
                var cluster = travelBucket.Cluster;
                var statement = "SELECT id,name,iata,icao,callsign, country FROM `travel-sample` WHERE type ='airline'";
                var result = await cluster.QueryAsync<Airline>(statement);
                return await result.ToListAsync();
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<IAsyncEnumerable<Airline>> GetAirlinesAsync()
        {
            try
            {
                var cluster = await _clusterProvider.GetClusterAsync();
                var statement = "SELECT id,name,iata,icao,callsign, country FROM `travel-sample` WHERE type ='airline'";
                var result = await cluster.QueryAsync<Airline>(statement);
                return result.Rows;
            }
            catch(Exception ex)
            {

            }
            return null;
        }


        public Task SaveAirlines(IEnumerable<Airline> airlines)
        {
            return Task.CompletedTask;
        }
    }
}
