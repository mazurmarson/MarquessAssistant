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
using Microsoft.AspNetCore.SignalR;
using MarqueesAssistant.API.signalR;

namespace MarqueesAssistant.API.Controllers
{
    [Authorize(Roles = "admin,kierownik,pracownik")]
    [ApiController]
    [Route("api/workers/{workerId}/[controller]")]
    public class MessagesController : ControllerBase
    {
       
        private readonly IMessRepo _repo;
        private readonly IWorkerRepo _workerRepo;
        private readonly IMapper _mapper;

         private readonly IHubContext<MessageHub, IMessageHubClient> mHubContext;
        public MessagesController( IMapper mapper, IMessRepo repo, IWorkerRepo workerRepo, IHubContext<MessageHub, IMessageHubClient> hubContext)
        {
            _workerRepo = workerRepo;
            _mapper = mapper;
            _repo = repo;
             mHubContext = hubContext;
        }

        [HttpGet("/{id}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessage(int workerId, int messageId)
        {
            if(workerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized();

            var message = await _repo.GetMessage(messageId);

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
            // Worker worker = await _workerRepo.GetWorker(workerId);
            _repo.Add<Message>(message);
            
            if(await _repo.SaveAll())
            {
               // await mHubContext.Clients.All.BroadcastMessage(message.Content);
               // await mHubContext.Clients.All.MessageSended();
               await mHubContext.Clients.User(workerId.ToString()).MessageSended();
                return CreatedAtRoute("GetMessage", new { id = message.Id}, message);
            }

            return BadRequest("Wystąpiły błedy");
           

         
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
            var messages = _repo.getFirstSentences(workerId);

            return Ok(messages);
        }
        
        [HttpGet("conversation/{id}", Name = "GetConversation")]
        public async Task<IActionResult> GetConversation(int workerId, int id)
        {
             if(workerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized();
            
            // var messages = await _context.Messages.Where(x =>
            // (x.SenderId == workerId || x.RecipientId == workerId ) && 
            // ( x.SenderId == id || x.RecipientId == id ) ).OrderByDescending(x => x.SendDate).ToListAsync();

             var messages = await _repo.GetConversation(workerId, id);
            // await _context.Messages.Where(x =>
            // (x.SenderId == workerId || x.RecipientId == workerId ) && 
            // ( x.SenderId == id || x.RecipientId == id ) ).OrderByDescending(x => x.SendDate)
            // .Include(Message => Message.Recipient)
            // .Include(Message => Message.Sender)
            // .Select(Message => new MessageDisplayDto(Message)).ToListAsync();
         // await mHubContext.Clients.All.BroadcastMessage("hehe");
           // await mHubContext.Clients.All.GetConversation(messages);

            return Ok(messages);
        }


        [HttpPost("read/{id}")]
        public async Task<IActionResult> ReadMessage(int workerId, int id)
        {
            if(workerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized();

            // var message = await _context.Messages.FirstOrDefaultAsync( m => m.Id == id);
             var message = await _repo.GetMessage(id);

            if(message.RecipientId  != workerId)
            return Unauthorized();

            message.IsRead = true;

            await _repo.SaveAll();
          //  await mHubContext.Clients.All.BroadcastMessage(message.Content);
            return Ok();
        }

        [HttpGet("anyMessages")]
        public async Task<IActionResult> AnyMessages(int workerId)
        {
            
            if(workerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized();

           int numOfMess = await _repo.CountMessagges(workerId);

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