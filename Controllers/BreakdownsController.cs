using System;
using System.Threading.Tasks;
using MarqueesAssistant.API.Data;
using MarqueesAssistant.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarqueesAssistant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BreakdownsController:ControllerBase
    {
        private readonly IBreakdownRepo _repo;
        private readonly IEquipmentRepo _equipmentRepo;

        public BreakdownsController(IBreakdownRepo repo, IEquipmentRepo equipmentRepo)
        {
            _repo = repo;
            _equipmentRepo = equipmentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetBreakdowns()
        {
         //   var breakdowns = await _repo.GetBreakdowns();
           var breakdowns = await _repo.GetBreakdownsDisplay();
            return Ok(breakdowns);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBreakdown(int id)
        {
            var breakdown = await _repo.GetBreakdown(id);
            return Ok(breakdown);
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> AddBreakdown(int id, Breakdown breakdown)
        {
            Equipment equipment = await _equipmentRepo.GetEquipment(id);
            var breakdownToCreate = new Breakdown
            {
                Id = breakdown.Id,
                EquipmentId = equipment.Id,
                Description = breakdown.Description,
                AccitdentDate = breakdown.AccitdentDate,
                RepairdDate = breakdown.RepairdDate
            };

            _repo.Add<Breakdown>(breakdownToCreate);
            if(await _repo.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Nie można dodać awarii");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBreakdown(int id)
        {
            Breakdown breakdown = await _repo.GetBreakdown(id);
            _repo.Delete(breakdown);

            if(await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception("Nie można usunąć awarii");
        }

        [HttpPut]
        public async Task<IActionResult> EditBreakdown(Breakdown breakdown)
        {
            _repo.Edit<Breakdown>(breakdown);
            if(await _repo.SaveAll())
            {
                return NoContent();
            }
            return BadRequest("Nie można edytować awarii");
        }

        
    }
}