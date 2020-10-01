using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using MarqueesAssistant.API.Data;
using MarqueesAssistant.API.Dtos;
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

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public WorkersController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkers()
        {
            var workers = await _context.Workers.ToListAsync();
            var workersToReturn = _mapper.Map<IEnumerable<WorkerDisplayDto>>(workers);
            return Ok(workersToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkers(int id)
        {
            var worker = await _context.Workers.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(worker);
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }


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