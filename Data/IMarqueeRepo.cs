using System.Collections.Generic;
using System.Threading.Tasks;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Helpers;
using MarqueesAssistant.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarqueesAssistant.API.Data
{
    public interface IMarqueeRepo : IGenRepo
    {
         Task<IEnumerable<Marquee>> GetMarquees();
         
          Task<PagedList<MarqueeDisplayDto>> GetMarqueesListedSearchedSorted(PageParameters pageParameters, string searchString, int sortBy);
         Task<Marquee> GetMarquee(int id);

        
        //  Task<Marquee> AddMarquee(Marquee marquee);
        //  Task<IActionResult> DeleteMarquee(int id);
       
    }
}