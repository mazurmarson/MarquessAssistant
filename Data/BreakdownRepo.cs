using System.Collections.Generic;
using System.Threading.Tasks;
using MarqueesAssistant.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MarqueesAssistant.API.Dtos;
using System;
using MarqueesAssistant.API.Helpers;

namespace MarqueesAssistant.API.Data
{
    public class BreakdownRepo : GenRepo, IBreakdownRepo
    {
        private readonly DataContext _context;

        public BreakdownRepo(DataContext context) :base(context)
        {
            _context = context;
        }
        public async Task<Breakdown> GetBreakdown(int id)
        {
            var breakdown = await _context.Breakdowns.FirstOrDefaultAsync(x => x.Id == id);
            return breakdown;
        }

        public async Task<IEnumerable<Breakdown>> GetBreakdowns()
        {
            var breakdowns = await _context.Breakdowns.ToListAsync();
            return breakdowns;
        }

        public async Task<IEnumerable<BreakdownDisplayDto>> GetBreakdownsDisplay()
        {
            var breakdowns = await _context.Breakdowns.ToListAsync();
            var equipments = await _context.Equipments.ToListAsync();

            var result = from b in breakdowns
                        join e in equipments on b.EquipmentId equals e.Id
                        where e.Id == b.EquipmentId
                        select new BreakdownDisplayDto
                        {
                            Id = b.Id,
                            Description = b.Description,
                            AccitdentDate = b.AccitdentDate,
                            RepairdDate = b.RepairdDate,
                            EquipmentName = e.Name

                        };

            return result;            
        }

        public async Task<PagedList<BreakdownDisplayDto>> GetBreakDownsListedSearchedSorted(PageParameters pageParameters, string searchString, int sortBy, DateTime? startRange, DateTime? endRange)
        {
            

             if(searchString == null)
            {
                searchString = "";
            }
            if(sortBy.Equals(null))
            {
                sortBy = 0;
            }

            searchString = searchString.ToLower();
            
            var breakdowns = await _context.Breakdowns.ToListAsync();
            var equipments = await _context.Equipments.ToListAsync();

            List<BreakdownDisplayDto> breakdownsToReturn;

                        var result = from b in breakdowns
                        join e in equipments on b.EquipmentId equals e.Id
                        where e.Id == b.EquipmentId
                        select new BreakdownDisplayDto
                        {
                            Id = b.Id,
                            Description = b.Description,
                            AccitdentDate = b.AccitdentDate,
                            RepairdDate = b.RepairdDate,
                            EquipmentName = e.Name

                        };

            result = result.Where(x => x.Description.ToLower().Contains(searchString) ||
            x.EquipmentName.ToLower().Contains(searchString) || x.AccitdentDate.ToString().Contains(searchString) ||
            x.RepairdDate.Date.ToString().Contains(searchString));

            if(startRange != null)
            {
                result = result.Where(x => x.AccitdentDate >= startRange || x.RepairdDate >= startRange);
            }

            if(endRange != null)
             {
                result = result.Where(x => x.AccitdentDate <= endRange || x.RepairdDate <= endRange);
              
            }

            switch(sortBy)
            {
                case 1:
                    breakdownsToReturn = result.OrderBy(x => x.Description).ToList();
                    return PagedList<BreakdownDisplayDto>.ToPagedList(breakdownsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                
                case 2:
                    breakdownsToReturn = result.OrderByDescending(x => x.Description).ToList();
                    return PagedList<BreakdownDisplayDto>.ToPagedList(breakdownsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                
                case 3:
                     breakdownsToReturn = result.OrderBy(x => x.EquipmentName).ToList();
                    return PagedList<BreakdownDisplayDto>.ToPagedList(breakdownsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                
                case 4:
                    breakdownsToReturn = result.OrderByDescending(x => x.EquipmentName).ToList();
                    return PagedList<BreakdownDisplayDto>.ToPagedList(breakdownsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                
                case 5:
                    breakdownsToReturn = result.OrderBy(x => x.AccitdentDate).ToList();
                    return PagedList<BreakdownDisplayDto>.ToPagedList(breakdownsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                
                case 6:
                    breakdownsToReturn = result.OrderByDescending(x => x.AccitdentDate).ToList();
                    return PagedList<BreakdownDisplayDto>.ToPagedList(breakdownsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                
                case 7:
                    breakdownsToReturn = result.OrderBy(x => x.RepairdDate).ToList();
                    return PagedList<BreakdownDisplayDto>.ToPagedList(breakdownsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                
                case 8:
                    breakdownsToReturn = result.OrderByDescending(x => x.RepairdDate).ToList();
                    return PagedList<BreakdownDisplayDto>.ToPagedList(breakdownsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
            }

                    breakdownsToReturn = result.OrderBy(x => x.Description).ToList();
                    return PagedList<BreakdownDisplayDto>.ToPagedList(breakdownsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
        }


    }
}