using System;
using System.Linq;
using System.Threading.Tasks;
using MarqueesAssistant.API.Data;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarqueesAssistant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
  
    public class MarqueesController:ControllerBase
    {

        private readonly IMarqueeRepo _repo;
        private readonly IEventRepo _eventRepo;

        public MarqueesController(IMarqueeRepo repo, IEventRepo eventRepo)
        {
            _eventRepo = eventRepo;
            _repo = repo;
        }
        [Authorize(Roles = "admin, kierownik, pracownik")]
        [HttpGet]
        public async Task<IActionResult> GetMarquees()
        {
            var marquees = await _repo.GetMarquees();
            var events = await _eventRepo.GetEvents();
        
            var result = from m in marquees
                        join e in events on m.EventId equals e.Id
                        where m.EventId == e.Id
                        select new MarqueeDisplayDto
                        {
                            Id = m.Id,
                            EventName = e.Name,
                            Width = m.Width,
                            Length = m.Length,
                          //  UpDate = m.UpDate,
                           // DownDate = m.DownDate,
                        //   Description = m.description,
                            IsUp = m.IsUp,
                            IsDown = m.IsDown


                        };

            return Ok(result);
        }
        [Authorize(Roles = "admin, kierownik, pracownik")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var marquee = await _repo.GetMarquee(id);
            return Ok(marquee);
        }
        [Authorize(Roles = "admin, kierownik")]
        [HttpPost("{id:int}")]
        public async Task<IActionResult> AddMarquee(int id, Marquee marquee)
        {
            Event eventt = await _eventRepo.GetEvent(id);
            var marqueeToCreate = new Marquee
            {
                EventId = marquee.EventId,
                Width = marquee.Width,
                Length = marquee.Length,
          //      UpDate = marquee.UpDate,
          //      DownDate = marquee.DownDate,
                IsUp = marquee.IsUp,
                IsDown = marquee.IsDown,
                Event = eventt
            };

            _repo.Add<Marquee>(marqueeToCreate);

            if(await _repo.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Nie można dodać namiotu");
            
   

            
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
        [Authorize(Roles = "admin, kierownik")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarquee(int id)
        {
            
            Marquee marquee = await _repo.GetMarquee(id);
            _repo.Delete(marquee);
            // _context.Marquees.Remove(marquee);
            if(await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception("Błąd podczas usuwania namiotu");
        }
        [Authorize(Roles = "admin, kierownik")]
        [HttpPut]
        public async Task<IActionResult> EditMarquee(Marquee marquee)
        {
            _repo.Edit(marquee);
            if(await _repo.SaveAll())
            {
                return NoContent();
            }

            
            return NoContent();
           // throw new Exception("Aktualizacja namiotu nie powiodła sie przy zapisywaniu do bazy");
        }


    }
}