using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using MarqueesAssistant.API.Data;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Helpers;
using MarqueesAssistant.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarqueesAssistant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WorkersController: ControllerBase
    {

        private readonly IWorkerRepo _repo;
        private readonly IMapper _mapper;
        public WorkersController(IWorkerRepo repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        // [HttpGet]
        // [Authorize(Roles = "admin, kierownik, pracownik")]
        // public async Task<IActionResult> GetWorkers()
        // {
        //     var workers = await _repo.GetWorkers();
        //     var workersToReturn = _mapper.Map<IEnumerable<WorkerDisplayDto>>(workers);
        //     return Ok(workersToReturn);
        // }

        [HttpGet]
        [Authorize(Roles = "admin, kierownik, pracownik")]
        public async Task<IActionResult> GetWorkers([FromQuery] OwnerParameters ownerParameters)
        {
            
            var workers = await _repo.GetWorkers2(ownerParameters);
         //   var workers = await _repo.GetWorkers();
        //    var workersToReturn = _mapper.Map<PagedList<WorkerDisplayDto>>(workers);

       //    Pagger<WorkerDisplayDto> workersToReturn2 = new Pagger<WorkerDisplayDto>(workersToReturn);
            Pagger<WorkerDisplayDto> workersToReturn = new Pagger<WorkerDisplayDto>(workers);
            return Ok(workersToReturn);
        }
       
        [HttpGet("searchWorker")]
        public async Task<IActionResult> GetSearchedWorkers([FromQuery] OwnerParameters ownerParameters, string searchString, int sortBy)
        {
            if(searchString == null)
            {
                searchString = "";
            }
            if(sortBy.Equals(null))
            {
                sortBy = 0;
            }
          //  var workers = await _repo.GetSearchedWorkers(ownerParameters, searchString);
          var workers = await _repo.GetSearchedWorkersAndSorted(ownerParameters, searchString, sortBy);
            Pagger<WorkerDisplayDto> workersToReturn = new Pagger<WorkerDisplayDto>(workers);
            return Ok(workersToReturn);
        }



        [Authorize(Roles = "admin, kierownik, pracownik")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorker(int id)
        {
            var worker = await _repo.GetWorker(id);
            return Ok(worker);
        }
        [HttpPut]
        public async Task<IActionResult> EditWorker(Worker worker)
        {
            _repo.Edit(worker);
            if(await _repo.SaveAll())
            {
                return Ok();
            }

            return NoContent();
            
        }
        
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(int id)
        {

             if(id == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized("Nie możesz usunąć samego siebie");

            Worker worker = await _repo.GetWorker(id);
            _repo.Delete(worker);
            

            if(await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception("Błąd podczas usuwania pracownika");
        }

        // public WorkerDisplayDto workerToDto(Worker worker)
        // {
        //     WorkerDisplayDto workerDisplayDto = 
        // }



        // [HttpGet]
        // public async Task<IActionResult> GetMessagesForWorker(int workerId)
        // {
        //     if(workerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        //     return Unauthorized();

        //     var messages = await GetMessages(workerId);

        //     return Ok(messages);
        // }
        
        // public async Task<IActionResult> GetMessages(int workerId)
        // {
        //     var messages = await _context.Messages.Include(u => u.Sender)
        //                                     .Include(u => u.Recipient).ToListAsync();

        //     messages = messages.OrderByDescending(x => x.SendDate).ToList();

        //     return Ok(messages);


        // }

        

    }
}