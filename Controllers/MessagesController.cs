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
using MarqueesAssistant.API.Dtos;

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
            // var users = 
            int id = _context.Workers.Select(u => u.Id).Max();
            

            for(int i = 0; i < id; i++)
            {
                if(workerId != i)
                {
                var message = _context.Messages.Where(x =>
                 (x.SenderId == workerId || x.RecipientId == workerId ) && 
                ( x.SenderId == i || x.RecipientId == i ) ).OrderByDescending(x => x.SendDate)
                .Take(1).ToList();
                // .Select(m => new Message{
                //     Id = m.Id,
                //     SenderId = m.SenderId,
                //     RecipientId = m.RecipientId,
                //     SendDate = m.SendDate,
                //     Content = m.Content

                // });
                
                Message messageObject = message.FirstOrDefault();

                if(messageObject != null)
                MessagesList.Add(messageObject);
                
                }

            }

            return Ok(MessagesList);
        }
        
        [HttpGet("conversation/{id}", Name = "GetConversation")]
        public async Task<IActionResult> GetConversation(int workerId, int id)
        {
             if(workerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized();
            
            // var messages = await _context.Messages.Where(x =>
            // (x.SenderId == workerId || x.RecipientId == workerId ) && 
            // ( x.SenderId == id || x.RecipientId == id ) ).OrderByDescending(x => x.SendDate).ToListAsync();

            var messages = await _context.Messages.Where(x =>
            (x.SenderId == workerId || x.RecipientId == workerId ) && 
            ( x.SenderId == id || x.RecipientId == id ) ).OrderByDescending(x => x.SendDate)
            .Include(Message => Message.Recipient)
            .Include(Message => Message.Sender)
            .Select(Message => new MessageDisplayDto(Message)).ToListAsync();
           

            return Ok(messages);
        }


        [HttpPost("read/{id}")]
        public async Task<IActionResult> ReadMessage(int workerId, int id)
        {
            if(workerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized();

            var message = await _context.Messages.FirstOrDefaultAsync( m => m.Id == id);

            if(message.RecipientId  != workerId)
            return Unauthorized();

            message.IsRead = true;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("anyMessages")]
        public async Task<IActionResult> AnyMessages(int workerId)
        {
            if(workerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized();

            int numOfMess = await _context.Messages.Where(x => x.RecipientId == workerId && x.IsRead == false).CountAsync();

            return Ok(numOfMess);
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