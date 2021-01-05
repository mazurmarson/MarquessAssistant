using System.Collections.Generic;
using System.Threading.Tasks;
using MarqueesAssistant.API.Helpers;
using MarqueesAssistant.API.Models;

namespace MarqueesAssistant.API.Data
{
    public interface IPlaceRepo : IGenRepo
    {
        Task<IEnumerable<Place>> GetPlaces();
        Task<PagedList<Place>> GetPlacesListed(PageParameters pageParameters);
        Task<PagedList<Place>> GetPlacesListedSearchedSorted(PageParameters pageParameters, string searchString, int sortBy);
        Task<Place> GetPlace(int id);
    }
}