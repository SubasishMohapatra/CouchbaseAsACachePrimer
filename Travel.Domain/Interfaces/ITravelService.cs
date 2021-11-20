using System.Collections.Generic;
using System.Threading.Tasks;
using Travel.Domain;

namespace Travel.Domain.Interfaces
{
    public interface ITravelService
    {
        Task<IEnumerable<Airline>> GetAirlines();
        Task<IAsyncEnumerable<Airline>> GetAirlinesAsync();

        Task SaveAirlines(IEnumerable<Airline> airlines);
    }
}
