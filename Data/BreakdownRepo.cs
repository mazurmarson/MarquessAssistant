using System.Collections.Generic;
using System.Threading.Tasks;
using MarqueesAssistant.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MarqueesAssistant.API.Dtos;

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
    }
}