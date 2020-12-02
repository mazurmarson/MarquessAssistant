using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarqueesAssistant.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MarqueesAssistant.API.Data
{
    public class EquipmentRepo : GenRepo, IEquipmentRepo
    {
        private readonly DataContext _context;

        public EquipmentRepo(DataContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Equipment> GetEquipment(int id)
        {
            var equipment = await _context.Equipments.FirstOrDefaultAsync(x => x.Id == id);
            return equipment;
        }

        public async Task<IEnumerable<Breakdown>> GetEquipmentBreakdowns(int id)
        {
            var equipmentBreakdowns = await _context.Breakdowns.Where(x => x.EquipmentId == id).ToListAsync();
            return equipmentBreakdowns;
        }

        public async Task<IEnumerable<Equipment>> GetEquipments()
        {
            var equipments = await _context.Equipments.ToListAsync();
            return equipments;
        }
    }
}