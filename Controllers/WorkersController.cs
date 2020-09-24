using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MarqueesAssistant.API.Data;
using MarqueesAssistant.API.Dtos;
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

    }
}