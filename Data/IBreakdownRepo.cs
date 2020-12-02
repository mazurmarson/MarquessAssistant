using System.Collections.Generic;
using System.Threading.Tasks;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Models;

namespace MarqueesAssistant.API.Data
{
    public interface IBreakdownRepo : IGenRepo
    {
        Task<IEnumerable<Breakdown>> GetBreakdowns();

        Task<IEnumerable<BreakdownDisplayDto>> GetBreakdownsDisplay();
        
        Task<Breakdown> GetBreakdown(int id);
    }
}