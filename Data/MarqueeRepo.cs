using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Helpers;
using MarqueesAssistant.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarqueesAssistant.API.Data
{
    public class MarqueeRepo : GenRepo, IMarqueeRepo
    {
        private readonly DataContext _context;
        public MarqueeRepo(DataContext context) : base(context)
        {
            _context = context;
        }
        // public async Task<Marquee> AddMarquee(Marquee marquee)
        // {
        //     await _context.Marquees.AddAsync(marquee);
        //     await _context.SaveChangesAsync();
        //     return marquee;
        // }



        // public async Task<IActionResult> DeleteMarquee(int id)
        // {
        //     Marquee marquee = await _context.Marquees.FirstOrDefaultAsync(x => x.Id == id);

        //     _context.Marquees.Remove(marquee);
        //     await _context.SaveChangesAsync();


        //     return Ok();
        // }



        public async Task<IEnumerable<Marquee>> GetMarquees()
        {
            
            var marquees = await _context.Marquees.ToListAsync();
            return marquees;
        }

        public async Task<Marquee> GetMarquee(int id)
        {
            
            var marquee = await _context.Marquees.FirstOrDefaultAsync(x => x.Id == id);
            return marquee;
        }

        public async Task<PagedList<MarqueeDisplayDto>> GetMarqueesListedSearchedSorted(PageParameters pageParameters, string searchString, int sortBy)
        {
            var marquees = await _context.Marquees.ToListAsync();
            var events = await _context.Events.ToListAsync();

            searchString = searchString.ToLower();

            List<MarqueeDisplayDto> marqueesToReturn;

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
                            Description = m.Description,
                            IsUp = m.IsUp,
                            IsDown = m.IsDown

                        };

            result = result.Where(x => x.EventName.ToLower().Contains(searchString) || 
            x.Length.ToString().Contains(searchString) || x.Width.ToString().Contains(searchString) );
            //|| x.Description.ToLower().Contains(searchString)

            //Event name asc 1

            //Event name desc 2
            //Width asc 3
            //Width desc 4
            //Length asc 5
            //Length desc 6
            //Description asc 7
            //DEscription desc 8
            //Is up asc 9
            //is up desc 10
            //Is down asc 11
            //Is down desc 12

            switch(sortBy)
            {
                case 1:
                marqueesToReturn = result.OrderBy(x => x.EventName).ToList();
                return PagedList<MarqueeDisplayDto>.ToPagedList(marqueesToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                
                case 2:
                marqueesToReturn = result.OrderByDescending(x => x.EventName).ToList();
                return PagedList<MarqueeDisplayDto>.ToPagedList(marqueesToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                
                case 3:
                marqueesToReturn = result.OrderBy(x => x.Width).ToList();
                return PagedList<MarqueeDisplayDto>.ToPagedList(marqueesToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                
                case 4:
                marqueesToReturn = result.OrderByDescending(x => x.Width).ToList();
                return PagedList<MarqueeDisplayDto>.ToPagedList(marqueesToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                
                case 5:
                marqueesToReturn = result.OrderBy(x => x.Length).ToList();
                return PagedList<MarqueeDisplayDto>.ToPagedList(marqueesToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                
                case 6:
                marqueesToReturn = result.OrderByDescending(x => x.Length).ToList();
                return PagedList<MarqueeDisplayDto>.ToPagedList(marqueesToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                
                // case 7:
                // marqueesToReturn = result.OrderBy(x => x.Description).ToList();
                // return PagedList<MarqueeDisplayDto>.ToPagedList(marqueesToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                
                // case 8:
                // marqueesToReturn = result.OrderByDescending(x => x.Description).ToList();
                // return PagedList<MarqueeDisplayDto>.ToPagedList(marqueesToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                
                case 7:
                marqueesToReturn = result.OrderBy(x => x.IsUp).ToList();
                return PagedList<MarqueeDisplayDto>.ToPagedList(marqueesToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                
                case 8:
                marqueesToReturn = result.OrderByDescending(x => x.IsUp).ToList();
                return PagedList<MarqueeDisplayDto>.ToPagedList(marqueesToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                
                case 9:
                marqueesToReturn = result.OrderBy(x => x.IsDown).ToList();
                return PagedList<MarqueeDisplayDto>.ToPagedList(marqueesToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                
                case 10:
                marqueesToReturn = result.OrderByDescending(x => x.IsDown).ToList();
                return PagedList<MarqueeDisplayDto>.ToPagedList(marqueesToReturn, pageParameters.PageNumber, pageParameters.PageSize);
            
            }
        marqueesToReturn = result.ToList();
        return PagedList<MarqueeDisplayDto>.ToPagedList(marqueesToReturn, pageParameters.PageNumber, pageParameters.PageSize);

        }
    }
}