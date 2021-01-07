using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MarqueesAssistant.API.Data;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Helpers;
using MarqueesAssistant.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace MarqueesAssistant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EventsController : ControllerBase
    {
        private readonly IEventRepo _repo;
        private readonly IPlaceRepo _placeRepo;
        private readonly IMapper _mapper;


        private readonly IMarqueeRepo _marqueeRepo;
        public EventsController(IEventRepo repo, IMapper mapper, IMarqueeRepo marqueeRepo, IPlaceRepo placeRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _placeRepo = placeRepo;
            _marqueeRepo = marqueeRepo;
        }

        [Authorize(Roles = "admin, kierownik, pracownik")]
        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] PageParameters pageParameters, string searchString, int sortBy, DateTime startRange, DateTime endRange)
        {

            if (searchString == null)
            {
                searchString = "";
            }
            if (sortBy.Equals(null))
            {
                sortBy = 0;
            }

            var events = await _repo.GetEventsDisplay(pageParameters, searchString, sortBy, startRange, endRange);
            Pagger<EventDisplayDto> eventsToReturn = new Pagger<EventDisplayDto>(events);

            return Ok(eventsToReturn);
        }


        [Authorize(Roles = "admin, kierownik, pracownik")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var eventt = await _repo.GetEvent(id);
            return Ok(eventt);
        }

        [Authorize(Roles = "admin, kierownik, pracownik")]
        [HttpGet("stuff/{id}")]
        public async Task<IActionResult> GetEventStuff(int id)
        {
            var marquees = await _repo.GetEventStuff(id);

            return Ok(marquees);


        }

        [Authorize(Roles = "admin, kierownik")]
        [HttpPost("{id:int}")]
        public async Task<IActionResult> AddEvent(int id, Event eventt)
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

            if (await _repo.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Nie można dodać wydarzenia");


        }

        [Authorize(Roles = "admin, kierownik")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            Event eventt = await _repo.GetEvent(id);

            _repo.Delete(eventt);

            if (await _repo.SaveAll())
            {
                return NoContent();
            }
            throw new Exception("Nie można usunąć wydarzenia");
        }
        
        [Authorize(Roles = "admin, kierownik")]
        [HttpPut]
        public async Task<IActionResult> EditEvent(Event eventt)
        {
            _repo.Edit(eventt);
            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception("Nie można edytować wydarzenia");
        }
        [Authorize(Roles = "admin, kierownik, pracownik")]
        [HttpGet("getEventPlaceName/{id}")]
        public async Task<IActionResult> getEventPlaceName(int id)
        {
            var name = await _repo.GetEventPlaceName(id);
            MyString myString = new MyString(name);
            if (name != null)
            {
                return Ok(myString);
            }

            throw new Exception("Nie można pobrać miejsca");
        }

    }
}