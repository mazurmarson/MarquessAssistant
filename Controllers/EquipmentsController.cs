using System.Threading.Tasks;
using MarqueesAssistant.API.Data;
using MarqueesAssistant.API.Helpers;
using MarqueesAssistant.API.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "admin, kierownik, pracownik")]
        [HttpGet]
        public async Task<IActionResult> GetEquipments([FromQuery]PageParameters pageParameters, string searchString, int sortBy)
        {
           if(searchString == null)
            {
                searchString = "";
            }
            if(sortBy.Equals(null))
            {
                sortBy = 0;
            }
            
            var equipments = await _repo.GetEquipmentsListedSearchedSorted(pageParameters, searchString, sortBy);
            Pagger<Equipment> equipmentsToReturn = new Pagger<Equipment>(equipments);
            return Ok(equipmentsToReturn);
        }
        [Authorize(Roles = "admin, kierownik, pracownik")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEquipment(int id)
        {

            
            var equipment = await _repo.GetEquipment(id);
            return Ok(equipment);

        }
        [Authorize(Roles = "admin, kierownik")]
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
        [Authorize(Roles = "admin, kierownik")]
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
        [Authorize(Roles = "admin, kierownik")]    
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
        [Authorize(Roles = "admin, kierownik, pracownik")]
        [HttpGet("getEquipmentBreakdowns/{id}")]
        public async Task<IActionResult> getEquipmentBreakdowns([FromQuery]PageParameters pageParameters, string searchString, int id)
        {
            var equipmentsBreakdowns = await _repo.GetEquipmentBreakdowns(pageParameters,id);
            Pagger<Breakdown> equipmentsBreakdownsToReturn = new Pagger<Breakdown>(equipmentsBreakdowns);
            return Ok(equipmentsBreakdownsToReturn);
        }


    }
}