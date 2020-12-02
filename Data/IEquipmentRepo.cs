using System.Collections.Generic;
using System.Threading.Tasks;
using MarqueesAssistant.API.Models;

namespace MarqueesAssistant.API.Data
{
    public interface IEquipmentRepo : IGenRepo
    {
        Task<IEnumerable<Equipment>> GetEquipments();
        Task<Equipment> GetEquipment(int id);
        Task<IEnumerable<Breakdown>> GetEquipmentBreakdowns(int id);
    }
}