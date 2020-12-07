using System.Collections.Generic;
using System.Threading.Tasks;
using MarqueesAssistant.API.Helpers;
using MarqueesAssistant.API.Models;

namespace MarqueesAssistant.API.Data
{
    public interface IEquipmentRepo : IGenRepo
    {
        Task<IEnumerable<Equipment>> GetEquipments();

        Task<PagedList<Equipment>> GetEquipmentsListedSearchedSorted(PageParameters pageParameters, string searchString, int sortBy);
        Task<Equipment> GetEquipment(int id);
        Task<IEnumerable<Breakdown>> GetEquipmentBreakdowns(int id);
    }
}