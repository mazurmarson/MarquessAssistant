using System;
using System.Threading.Tasks;
using MarqueesAssistant.API.Data;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Helpers;
using MarqueesAssistant.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarqueesAssistant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BreakdownsController : ControllerBase
    {
        private readonly IBreakdownRepo _repo;
        private readonly IEquipmentRepo _equipmentRepo;

        public BreakdownsController(IBreakdownRepo repo, IEquipmentRepo equipmentRepo)
        {
            _repo = repo;
            _equipmentRepo = equipmentRepo;
        }

        [Authorize(Roles = "admin, kierownik, pracownik")]
        [HttpGet]
        public async Task<IActionResult> GetBreakdowns([FromQuery] PageParameters pageParameters, string searchString, int sortBy, DateTime? startRange, DateTime? endRange)
        {
            if (searchString == null)
            {
                searchString = "";
            }
            if (sortBy.Equals(null))
            {
                sortBy = 0;
            }
            var breakdowns = await _repo.GetBreakDownsListedSearchedSorted(pageParameters, searchString, sortBy, startRange, endRange);
            Pagger<BreakdownDisplayDto> breakdownsToReturn = new Pagger<BreakdownDisplayDto>(breakdowns);
            return Ok(breakdownsToReturn);
        }
        
        [Authorize(Roles = "admin, kierownik, pracownik")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBreakdown(int id)
        {
            var breakdown = await _repo.GetBreakdown(id);
            return Ok(breakdown);
        }

        [Authorize(Roles = "admin, kierownik")]
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
            if (await _repo.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Nie można dodać awarii");
        }
        [Authorize(Roles = "admin, kierownik")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBreakdown(int id)
        {
            Breakdown breakdown = await _repo.GetBreakdown(id);
            _repo.Delete(breakdown);

            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception("Nie można usunąć awarii");
        }
        [Authorize(Roles = "admin, kierownik")]
        [HttpPut]
        public async Task<IActionResult> EditBreakdown(Breakdown breakdown)
        {
            _repo.Edit<Breakdown>(breakdown);
            if (await _repo.SaveAll())
            {
                return NoContent();
            }
            return BadRequest("Nie można edytować awarii");
        }


    }
}