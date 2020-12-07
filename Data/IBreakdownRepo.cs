using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Helpers;
using MarqueesAssistant.API.Models;

namespace MarqueesAssistant.API.Data
{
    public interface IBreakdownRepo : IGenRepo
    {
        Task<IEnumerable<Breakdown>> GetBreakdowns();

        Task<IEnumerable<BreakdownDisplayDto>> GetBreakdownsDisplay();

        Task<PagedList<BreakdownDisplayDto>> GetBreakDownsListedSearchedSorted(PageParameters pageParameters, string searchString, int sortBy, DateTime? startRange, DateTime? endRange);
        
        Task<Breakdown> GetBreakdown(int id);
    }
}