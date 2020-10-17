using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MarqueesAssistant.API.Data;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MarqueesAssistant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController:ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public EventsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _context.Events.ToArrayAsync();
           // events = (List<Event>)(IEnumerable<Event>)events;
            
          //  var eventsDto = _mapper.Map<IEnumerable<EventDisplayDto>>(events);
            var places = await _context.Places.ToArrayAsync();

            var result = from e in events
                            join p in places on e.PlaceId equals p.Id
                            where p.Id == e.PlaceId
                            select new EventDisplayDto
                            {
                                Id = e.Id,
                                Name = e.Name,
                                StartDate = e.StartDate,
                                EndDate = e.EndDate,
                                PlaceId = e.PlaceId,
                                PlaceName = p.Town,
                                TypeOfEvent = e.TypeOfEvent
                            };
            

            // var workersToReturn = _mapper.Map<IEnumerable<WorkerDisplayDto>>(workers);
                                
            
            

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var eventt = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(eventt);
        }

        [HttpGet("stuff/{id}")]
        public async Task<IActionResult> GetEventStuff(int id)
        {
            var marquees = await _context.Marquees.Where(x => x.EventId == id).Include(x => x.Event).Select(Marquee => new MarqueesStuffDto(Marquee)).ToListAsync();
            return Ok(marquees);

            
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> AddEvent(int id,Event eventt )
        {
            Place place = await _context.Places.FirstOrDefaultAsync(x => x.Id == id);
            var eventToCreate = new Event
            {
                Name = eventt.Name,
                StartDate = eventt.StartDate,
                EndDate = eventt.EndDate,
                PlaceId = eventt.PlaceId,
                TypeOfEvent = eventt.TypeOfEvent,
                Place = place
            };

            await _context.Events.AddAsync(eventToCreate);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            Event eventt = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);
            _context.Events.Remove(eventt);

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditEvent(Event eventt)
        {
            _context.Events.Update(eventt);
            await _context.SaveChangesAsync();

            return Ok();
        }
        
    }
}