using System.Collections.Generic;
using System.Threading.Tasks;
using Travel.Domain;

namespace Travel.Domain.Interfaces
{
    public interface ITravelRepository
    {
        Task<IEnumerable<Airline>> GetAirlines();
        Task<IAsyncEnumerable<Airline>> GetAirlinesAsync();
        Task SaveAirlines(IEnumerable<Airline> airlines);
    }
}
