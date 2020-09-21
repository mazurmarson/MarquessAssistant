using System.Threading.Tasks;
using MarqueesAssistant.API.Data;
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
    }
}