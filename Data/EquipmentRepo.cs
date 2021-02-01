using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarqueesAssistant.API.Helpers;
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

        public async Task<PagedList<Breakdown>> GetEquipmentBreakdowns(PageParameters pageParameters ,int id)
        {
            var equipmentBreakdowns = await _context.Breakdowns
            .Where(x => x.EquipmentId == id)
            .OrderByDescending(x => x.AccitdentDate).ToListAsync();
           
            return PagedList<Breakdown>.ToPagedList(equipmentBreakdowns, 
            pageParameters.PageNumber, pageParameters.PageSize);
        }

        public async Task<IEnumerable<Equipment>> GetEquipments()
        {
            var equipments = await _context.Equipments.ToListAsync();
            return equipments;
        }

        public async Task<PagedList<Equipment>> GetEquipmentsListedSearchedSorted(PageParameters pageParameters, string searchString, int sortBy)
        {
             searchString = searchString.ToLower();
            List<Equipment> equipmentsToReturn;

            var equipments = await _context.Equipments.Where(x=> x.EquipmentType.ToLower().Contains(searchString) 
            || x.Name.ToLower().Contains(searchString)).ToListAsync();

            switch(sortBy)
            {
                case 1:
                equipmentsToReturn = equipments.OrderBy(x => x.Name).ToList();
                return PagedList<Equipment>.ToPagedList(equipmentsToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 2:
                equipmentsToReturn = equipments.OrderByDescending(x => x.Name).ToList();
                return PagedList<Equipment>.ToPagedList(equipmentsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
                case 3:
                equipmentsToReturn = equipments.OrderBy(x => x.EquipmentType).ToList();
                return PagedList<Equipment>.ToPagedList(equipmentsToReturn, pageParameters.PageNumber, pageParameters.PageSize);

                case 4:
                equipmentsToReturn = equipments.OrderByDescending(x => x.EquipmentType).ToList();
                return PagedList<Equipment>.ToPagedList(equipmentsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
            }

                equipmentsToReturn = equipments.OrderBy(x => x.Name).ToList();
                return PagedList<Equipment>.ToPagedList(equipmentsToReturn, pageParameters.PageNumber, pageParameters.PageSize);
        }
    }
}