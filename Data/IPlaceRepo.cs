using System.Collections.Generic;
using System.Threading.Tasks;
using MarqueesAssistant.API.Models;

namespace MarqueesAssistant.API.Data
{
    public interface IPlaceRepo : IGenRepo
    {
        Task<IEnumerable<Place>> GetPlaces();
         Task<Place> GetPlace(int id);
    }
}