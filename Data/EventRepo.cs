using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarqueesAssistant.API.Data
{
    public class EventRepo : GenRepo, IEventRepo
    {
        private readonly DataContext _context;

        public EventRepo(DataContext context) : base(context)
        {
            _context = context;
        }


        public Task<Event> GetEvent(int id)
        {
            var eventt = _context.Events.FirstOrDefaultAsync(x => x.Id == id);
            return eventt;
        }



        public async Task<IEnumerable<Event>> GetEvents()
        {
            var events = await _context.Events.ToListAsync();
            return events;
        }

        public async Task<IEnumerable<EventDisplayDto>> GetEventsDisplay()
        {
            var events = await _context.Events.ToListAsync();
            var places = await _context.Places.ToListAsync();

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

                            return result;
        }

        public async Task<IEnumerable<MarqueesStuffDto>> GetEventStuff(int id)
        {
            var marquees = await _context.Marquees.Where(x => x.EventId == id)
            .Include(x => x.Event).Select(Marquee => new MarqueesStuffDto(Marquee)).ToListAsync();

            return marquees;
        }

        public async Task<string> GetEventPlaceName(int id)
        {
            var events = await _context.Events.ToListAsync();
            var places = await _context.Places.ToListAsync();

           string placeName = events.Where(x => x.Id == id ).Select(x => x.Place.Town).FirstOrDefault();
           
            return placeName;
        }
    }
}