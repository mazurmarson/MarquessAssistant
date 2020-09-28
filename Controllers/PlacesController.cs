using System.Threading.Tasks;
using MarqueesAssistant.API.Data;
using MarqueesAssistant.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarqueesAssistant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlacesController:ControllerBase
    {
        private readonly DataContext _context;

        public PlacesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlaces()
        {
            var places = await _context.Places.ToListAsync();
            return Ok(places);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlace(int id)
        {
            var place = await _context.Places.FirstOrDefaultAsync(p => p.Id == id);
            return Ok(place);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlace(Place place)
        {
            var placeToCreate = new Place
            {
                Street = place.Street,
                Town = place.Town,
                FirstGradeDivision = place.FirstGradeDivision,
                SecondGradeDivision = place.SecondGradeDivision,
                ThirdGradeDivision = place.ThirdGradeDivision,
                PostCode = place.PostCode
            };

            await _context.Places.AddAsync(placeToCreate);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlace(int id)
        {
             Place place = await _context.Places.FirstOrDefaultAsync(x => x.Id == id);
             _context.Places.Remove(place);
   

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditPlace(Place place)
        {
            _context.Places.Update(place);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}