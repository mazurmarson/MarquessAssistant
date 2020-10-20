using System.Collections.Generic;
using System.Threading.Tasks;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarqueesAssistant.API.Data
{
    public interface IEventRepo : IGenRepo
    {
       Task<IEnumerable<Event>> GetEvents();

       Task<IEnumerable<EventDisplayDto>> GetEventsDisplay();
       Task<Event> GetEvent(int id);

       Task<IEnumerable<MarqueesStuffDto>> GetEventStuff(int id);

      
                 
                 

    }
}