using Travel.Domain.Interfaces;
using Travel.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Travel.Domain;

namespace Travel.WebAPI.Service
{
    public class TravelService : ITravelService
    {
        private readonly ITravelRepository _repository;

        public TravelService(ITravelRepository repository)
        {
            _repository = repository;
        }        

        public async Task<IEnumerable<Airline>> GetAirlines()
        {
            return await _repository.GetAirlines();
        }

        public async Task<IAsyncEnumerable<Airline>> GetAirlinesAsync()
        {
            return await _repository.GetAirlinesAsync();
        }

        public async Task SaveAirlines(IEnumerable<Airline> airlines)
        {
            await _repository.SaveAirlines(airlines);
        }
    }
};
