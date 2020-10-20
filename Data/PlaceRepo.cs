using System.Collections.Generic;
using System.Threading.Tasks;
using MarqueesAssistant.API.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}