using System;
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
        private readonly IEventRepo _repo;
        private readonly IPlaceRepo _placeRepo;
        private readonly IMapper _mapper;
       

        private readonly IMarqueeRepo _marqueeRepo;
        public EventsController(IEventRepo repo, IMapper mapper,  IMarqueeRepo marqueeRepo, IPlaceRepo placeRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _placeRepo  = placeRepo;
            _marqueeRepo = marqueeRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            // var events = await _repo.GetEvents();
           // events = (List<Event>)(IEnumerable<Event>)events;
            
          //  var eventsDto = _mapper.Map<IEnumerable<EventDisplayDto>>(events);
            // var places = await _context.Places.ToArrayAsync();

            // var result = from e in events
            //                 join p in places on e.PlaceId equals p.Id
            //                 where p.Id == e.PlaceId
            //                 select new EventDisplayDto
            //                 {
            //                     Id = e.Id,
            //                     Name = e.Name,
            //                     StartDate = e.StartDate,
            //                     EndDate = e.EndDate,
            //                     PlaceId = e.PlaceId,
            //                     PlaceName = p.Town,
            //                     TypeOfEvent = e.TypeOfEvent
            //                 };
            

            // var workersToReturn = _mapper.Map<IEnumerable<WorkerDisplayDto>>(workers);
                                
            var result = await _repo.GetEventsDisplay();
            

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var eventt = await _repo.GetEvent(id);
            return Ok(eventt);
        }

        [HttpGet("stuff/{id}")]
        public async Task<IActionResult> GetEventStuff(int id)
        {
            var marquees = await _repo.GetEventStuff(id);

            return Ok(marquees);

            
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> AddEvent(int id,Event eventt )
        {
            
            Place place = await _placeRepo.GetPlace(id);
            var eventToCreate = new Event
            {
                Name = eventt.Name,
                StartDate = eventt.StartDate,
                EndDate = eventt.EndDate,
                PlaceId = eventt.PlaceId,
                TypeOfEvent = eventt.TypeOfEvent,
                Place = place
            };

            _repo.Add<Event>(eventToCreate);

            if(await _repo.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Nie można dodać wydarzenia");

            
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            Event eventt = await _repo.GetEvent(id);
            _repo.Delete(eventt);

            if(await _repo.SaveAll())
            {
                return NoContent();
            }
             throw new Exception("Nie można usunąć wydarzenia");
        }

        [HttpPut]
        public async Task<IActionResult> EditEvent(Event eventt)
        {
            _repo.Edit(eventt);
            if(await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception("Nie można edytować wydarzenia");
        }
        
    }
}