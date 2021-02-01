using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Helpers;
using MarqueesAssistant.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarqueesAssistant.API.Data
{
    public interface IEventRepo : IGenRepo
    {
       Task<IEnumerable<Event>> GetEvents();

       Task<PagedList<EventDisplayDto>> GetEventsPagedSortedSearched(PageParameters pageParameters, 
       string searchString, int sortBy, DateTime? startRange, DateTime? endRange);
   
       Task<Event> GetEvent(int id);
       Task<IEnumerable<MarqueesStuffDto>> GetEventStuff(int id);
       Task<string> GetEventPlaceName(int id);

    }
}