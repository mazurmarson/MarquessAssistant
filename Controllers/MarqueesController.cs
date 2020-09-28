using System.Linq;
using System.Threading.Tasks;
using MarqueesAssistant.API.Data;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Models;
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
        public async Task<IActionResult> GetMarquees()
        {
            var marquees = await _context.Marquees.ToArrayAsync();
            var events = await _context.Events.ToArrayAsync();

            var result = from m in marquees
                        join e in events on m.EventId equals e.Id
                        where m.EventId == e.Id
                        select new MarqueeDisplayDto
                        {
                            Id = m.Id,
                            EventName = e.Name,
                            Width = m.Width,
                            Length = m.Length,
                            UpDate = m.UpDate,
                            DownDate = m.DownDate,
                            IsUp = m.IsUp,
                            IsDown = m.IsDown


                        };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var marquee = await _context.Marquees.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(marquee);
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> AddMarquee(int id, Marquee marquee)
        {
            Event eventt = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);
            var marqueeToCreate = new Marquee
            {
                EventId = marquee.EventId,
                Width = marquee.Width,
                Length = marquee.Length,
                UpDate = marquee.UpDate,
                DownDate = marquee.DownDate,
                IsUp = marquee.IsUp,
                IsDown = marquee.IsDown,
                Event = eventt
            };

            await _context.Marquees.AddAsync(marqueeToCreate);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        //         [HttpPost("register")]
        // public async Task<IActionResult> Register(WorkerRegisterDto workerRegisterDto)
        // {
        //     workerRegisterDto.Login = workerRegisterDto.Login.ToLower();

        //     if(await _repo.WorkerIsExist(workerRegisterDto.Login))
        //     {
        //         return BadRequest("Taki użytkownik już istnieje");
        //     }

        //     var workerToCreate = new Worker
        //     {
             
        //         Login = workerRegisterDto.Login
        //     };

        //     var createdWorker = await _repo.Register(workerToCreate, workerRegisterDto.Password);

        //     return StatusCode(201);

        // }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarquee(int id)
        {
            Marquee marquee = await _context.Marquees.FirstOrDefaultAsync(x => x.Id == id);
            _context.Marquees.Remove(marquee);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditMarquee(Marquee marquee)
        {
            _context.Marquees.Update(marquee);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}