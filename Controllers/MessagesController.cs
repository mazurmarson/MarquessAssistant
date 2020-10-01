using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using MarqueesAssistant.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MarqueesAssistant.API.Models;


namespace MarqueesAssistant.API.Controllers
{
    [ApiController]
    [Route("api/workers/{workerId}/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MessagesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("/{id}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessage(int workerId, int messageId)
        {
            if(workerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized();

            var message = await _context.Messages.FirstOrDefaultAsync(x => x.Id == messageId);

            if(message == null)
            return NotFound();

            return Ok(message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int workerId, Message message)
        {
            if(workerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized();

            message.SenderId = workerId;

            _context.Messages.Add(message);

            await _context.SaveChangesAsync();
           return CreatedAtRoute("GetMessage", new { id = message.Id}, message);

         
        }

        // [HttpGet] Pobiera wszystkie wiadomosci uzytkownika
        // public async Task<IActionResult> GetMessagesForWorker(int workerId)
        // {
        //     if(workerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        //     return Unauthorized();

        //     var messages = await _context.Messages.Where(x => x.SenderId == workerId || x.RecipientId == workerId ).ToListAsync();

            

        //     return Ok(messages);
        // }

        [HttpGet]
        public async Task<IActionResult> GetFirstSentence(int workerId)
        {
            if(workerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized();

            // var messages = await _context.Messages.Where((x => x.SenderId == workerId || x.RecipientId == workerId ))
            // .Where(x => x.SenderId != workerId || x.RecipientId != workerId).Take(1)
            // .ToListAsync();

            // var messages =  _context.Messages.Where((x => x.SenderId == workerId || x.RecipientId == workerId ));

            // var results = (from m in messages group m by m.RecipientId into grp
            // select new 
            // {
            //     RecipientId = grp.Key,

            // })
            List<Message> MessagesList = new List<Message>();
            // var users = _context.Workers.Select(u => u.Id).Max();
            int id = 5;
                        var messages =  _context.Messages.Where(x =>
            (x.SenderId == workerId || x.RecipientId == workerId ) && 
            ( x.SenderId == id || x.RecipientId == id ) ).OrderByDescending(x => x.SendDate).Take(1);


            return Ok(messages);
        }
        
        [HttpGet("conversation/{id}", Name = "GetConversation")]
        public async Task<IActionResult> GetConversation(int workerId, int id)
        {
             if(workerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized();
            
            var messages = await _context.Messages.Where(x =>
            (x.SenderId == workerId || x.RecipientId == workerId ) && 
            ( x.SenderId == id || x.RecipientId == id ) ).OrderByDescending(x => x.SendDate).ToListAsync();

            

            return Ok(messages);
        }

        

        //         public async Task<IActionResult> GetMessages(int workerId)
        // {
        //     var messages = await _context.Messages.Include(u => u.Sender)
        //                                     .Include(u => u.Recipient).ToListAsync();

        //     messages = messages.OrderByDescending(x => x.SendDate).ToList();

        //     return Ok(messages);


        // }

    }
}