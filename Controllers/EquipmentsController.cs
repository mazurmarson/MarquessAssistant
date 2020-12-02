using System.Threading.Tasks;
using MarqueesAssistant.API.Data;
using MarqueesAssistant.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarqueesAssistant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentsController:ControllerBase
    {
        private readonly IEquipmentRepo _repo;

        public EquipmentsController(IEquipmentRepo repo)
        {
            _repo = repo;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetEquipments()
        {
            var equipments = await _repo.GetEquipments();
            return Ok(equipments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEquipment(int id)
        {
            var equipment = await _repo.GetEquipment(id);
            return Ok(equipment);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            Equipment equipment = await _repo.GetEquipment(id);
            _repo.Delete(equipment);

            if(await _repo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("Nie można usunąć sprzętu");

        }

       [HttpPut("{id}")]

        public async Task<IActionResult> EditEquipment(Equipment equipment)
        {
            
            _repo.Edit(equipment);

            if(await _repo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("Nie można edytować sprzętu");

        }
        
        [HttpPost]
        public async Task<IActionResult> AddEquipment(Equipment equipment)
        {
            _repo.Add<Equipment>(equipment);
            if(await _repo.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Nie można dodać sprzętu");
        }

        [HttpGet("getEquipmentBreakdowns/{id}")]
        public async Task<IActionResult> getEquipmentBreakdowns(int id)
        {
            var equipmentsBreakdowns = await _repo.GetEquipmentBreakdowns(id);
            return Ok(equipmentsBreakdowns);
        }


    }
}