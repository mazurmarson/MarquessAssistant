using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Helpers;
using MarqueesAssistant.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NinjaNye.SearchExtensions;


namespace MarqueesAssistant.API.Data
{
    public class EventRepo : GenRepo, IEventRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EventRepo(DataContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
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
        public async Task<PagedList<EventDisplayDto>> GetEventsDisplay(PageParameters pageParameters, string searchString, int sortBy, DateTime? startRange, DateTime? endRange)
        {
            var events = await _context.Events.ToListAsync();
            var places = await _context.Places.ToListAsync();
            List<EventDisplayDto> eventsToReturn;

            var result = from e in events
                         join p in places on e.PlaceId equals p.Id
                         where p.Id == e.PlaceId
                         select new EventDisplayDto
                         {
                             Id = e.Id,
                             Name = e.Name,
                             StartDate = (DateTime)e.StartDate,
                             EndDate = (DateTime)e.EndDate,
                             PlaceId = e.PlaceId,
                             PlaceName = p.Town,
                             TypeOfEvent = e.TypeOfEvent
                         };

            result = result.Where(x => x.Name.ToLower().Contains(searchString) || x.PlaceName.ToLower().Contains(searchString) || x.TypeOfEvent.ToLower().Contains(searchString) || x.StartDate.ToString().Contains(searchString) || x.EndDate.ToString().Contains(searchString));

            if (startRange != default(DateTime))
            {
                result = result.Where(x => x.StartDate >= startRange || x.EndDate >= startRange);
            }

            if (endRange != default(DateTime))
            {
                result = result.Where(x => x.StartDate <= endRange || x.EndDate <= endRange);
            }


            switch (sortBy)
            {
                case 1:
                    eventsToReturn = result.OrderBy(x => x.Name).ToList();
                    return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 2:
                    eventsToReturn = result.OrderByDescending(x => x.Name).ToList();
                    return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 3:
                    eventsToReturn = result.OrderBy(x => x.StartDate).ToList();
                    return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 4:
                    eventsToReturn = result.OrderByDescending(x => x.StartDate).ToList();
                    return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 5:
                    eventsToReturn = result.OrderBy(x => x.EndDate).ToList();
                    return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 6:
                    eventsToReturn = result.OrderByDescending(x => x.EndDate).ToList();
                    return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 7:
                    eventsToReturn = result.OrderBy(x => x.PlaceName).ToList();
                    return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 8:
                    eventsToReturn = result.OrderByDescending(x => x.PlaceName).ToList();
                    return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 9:
                    eventsToReturn = result.OrderBy(x => x.TypeOfEvent).ToList();
                    return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                case 10:
                    eventsToReturn = result.OrderByDescending(x => x.TypeOfEvent).ToList(); ;
                    return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
            }

            eventsToReturn = result.ToList();
            return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
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

            string placeName = events.Where(x => x.Id == id).Select(x => x.Place.Town).FirstOrDefault();

            return placeName;
        }

        public async Task<PagedList<EventDisplayDto>> GetEventPagedSortedSearched(PageParameters pageParameters, string searchString, int sortBy)
        {
            searchString = searchString.ToLower();
            List<EventDisplayDto> eventsToReturn;

            var events = await _context.Events.ToListAsync();

            switch (sortBy)
            {
                case 1:
                    eventsToReturn = _mapper.Map<List<EventDisplayDto>>(events);

                    eventsToReturn = eventsToReturn.OrderBy(x => x.Name).ToList();

                    return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 2:

                    eventsToReturn = _mapper.Map<List<EventDisplayDto>>(events);

                    eventsToReturn = eventsToReturn.OrderByDescending(x => x.Name).ToList();

                    return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 3:

                    eventsToReturn = _mapper.Map<List<EventDisplayDto>>(events);

                    eventsToReturn = eventsToReturn.OrderBy(x => x.StartDate).ToList();

                    return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 4:

                    eventsToReturn = _mapper.Map<List<EventDisplayDto>>(events);

                    eventsToReturn = eventsToReturn.OrderByDescending(x => x.StartDate).ToList();

                    return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 5:

                    eventsToReturn = _mapper.Map<List<EventDisplayDto>>(events);

                    eventsToReturn = eventsToReturn.OrderBy(x => x.EndDate).ToList();

                    return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                case 6:

                    eventsToReturn = _mapper.Map<List<EventDisplayDto>>(events);

                    eventsToReturn = eventsToReturn.OrderByDescending(x => x.EndDate).ToList();



                    return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                case 7:

                    eventsToReturn = _mapper.Map<List<EventDisplayDto>>(events);

                    eventsToReturn = eventsToReturn.OrderBy(x => x.PlaceName).ToList();

                    return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                case 8:

                    eventsToReturn = _mapper.Map<List<EventDisplayDto>>(events);

                    eventsToReturn = eventsToReturn.OrderByDescending(x => x.PlaceName).ToList();



                    return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                case 9:

                    eventsToReturn = _mapper.Map<List<EventDisplayDto>>(events);

                    eventsToReturn = eventsToReturn.OrderBy(x => x.TypeOfEvent).ToList();

                    return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                case 10:

                    eventsToReturn = _mapper.Map<List<EventDisplayDto>>(events);

                    eventsToReturn = eventsToReturn.OrderByDescending(x => x.TypeOfEvent).ToList();

                    return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
            }

            eventsToReturn = _mapper.Map<List<EventDisplayDto>>(events);
            eventsToReturn = eventsToReturn.OrderBy(x => x.Name).ToList();

            return PagedList<EventDisplayDto>.ToPagedList(eventsToReturn, pageParameters.PageNumber, pageParameters.PageSize);


        }
    }
}