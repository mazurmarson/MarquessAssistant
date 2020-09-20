using System.Threading.Tasks;
using MarqueesAssistant.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarqueesAssistant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class MarqueesController:ControllerBase
    {
        private readonly DataContext _context;

        public MarqueesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var marquees = await _context.Marquees.ToListAsync();
            return Ok(marquees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var marquee = await _context.Marquees.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(marquee);
        }
    }
}