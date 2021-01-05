using System.Collections.Generic;
using System.Threading.Tasks;
using MarqueesAssistant.API.Helpers;
using MarqueesAssistant.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace MarqueesAssistant.API.Data
{
    public class PlaceRepo : GenRepo, IPlaceRepo
    {
        private readonly DataContext _context;


        public PlaceRepo(DataContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Place> GetPlace(int id)
        {
            var place = await _context.Places.FirstOrDefaultAsync(x => x.Id == id);
            return place;
        }

        public async Task<IEnumerable<Place>> GetPlaces()
        {
            var places = await _context.Places.ToListAsync();
            return places;
        }

        public async Task<PagedList<Place>> GetPlacesListed(PageParameters pageParameters)
        {
            var places = await _context.Places.ToListAsync();
            return PagedList<Place>.ToPagedList(places, pageParameters.PageNumber, pageParameters.PageSize);
        }

        public async Task<PagedList<Place>> GetPlacesListedSearchedSorted(PageParameters pageParameters, string searchString, int sortBy)
        {
            searchString = searchString.ToLower();
            List<Place> placesToReturn;

            var places = await _context.Places.Where(x => x.Street.ToLower().Contains(searchString) || x.Town.ToLower().Contains(searchString) || x.FirstGradeDivision.ToLower().Contains(searchString)
            || x.SecondGradeDivision.ToLower().Contains(searchString) || x.ThirdGradeDivision.ToLower().Contains(searchString) || x.PostCode.ToLower().Contains(searchString)).OrderBy(x => x.Town).ToListAsync();
            switch (sortBy)
            {
                case 1:
                    placesToReturn = places.OrderBy(x => x.Number).ToList();
                    return PagedList<Place>.ToPagedList(placesToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 2:
                    placesToReturn = places.OrderByDescending(x => x.Number).ToList();
                    return PagedList<Place>.ToPagedList(placesToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 3:
                    placesToReturn = places.OrderBy(x => x.Street).ToList();
                    return PagedList<Place>.ToPagedList(placesToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                case 4:
                    placesToReturn = places.OrderByDescending(x => x.Street).ToList();
                    return PagedList<Place>.ToPagedList(placesToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 5:
                    placesToReturn = places.OrderBy(x => x.Town).ToList();
                    return PagedList<Place>.ToPagedList(placesToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                case 6:
                    placesToReturn = places.OrderByDescending(x => x.Town).ToList();
                    return PagedList<Place>.ToPagedList(placesToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 7:
                    placesToReturn = places.OrderBy(x => x.FirstGradeDivision).ToList();
                    return PagedList<Place>.ToPagedList(placesToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 8:
                    placesToReturn = places.OrderByDescending(x => x.FirstGradeDivision).ToList();
                    return PagedList<Place>.ToPagedList(placesToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 9:
                    placesToReturn = places.OrderBy(x => x.SecondGradeDivision).ToList();
                    return PagedList<Place>.ToPagedList(placesToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 10:
                    placesToReturn = places.OrderByDescending(x => x.SecondGradeDivision).ToList();
                    return PagedList<Place>.ToPagedList(placesToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 11:
                    placesToReturn = places.OrderBy(x => x.ThirdGradeDivision).ToList();
                    return PagedList<Place>.ToPagedList(placesToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 12:
                    placesToReturn = places.OrderByDescending(x => x.ThirdGradeDivision).ToList();
                    return PagedList<Place>.ToPagedList(placesToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 13:
                    placesToReturn = places.OrderBy(x => x.PostCode).ToList();
                    return PagedList<Place>.ToPagedList(placesToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 14:
                    placesToReturn = places.OrderByDescending(x => x.PostCode).ToList();
                    return PagedList<Place>.ToPagedList(placesToReturn, pageParameters.PageNumber, pageParameters.PageSize);
            }

            return PagedList<Place>.ToPagedList(places, pageParameters.PageNumber, pageParameters.PageSize);
        }
    }
}