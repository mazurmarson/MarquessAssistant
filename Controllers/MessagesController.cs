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
using MarqueesAssistant.API.Helpers;

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
        public MessagesController(IMapper mapper, IMessRepo repo, IWorkerRepo workerRepo, IHubContext<MessageHub, IMessageHubClient> hubContext)
        {
            _workerRepo = workerRepo;
            _mapper = mapper;
            _repo = repo;
            mHubContext = hubContext;
        }

        [HttpGet("/{id}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessage(int workerId, int messageId)
        {
            if (workerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var message = await _repo.GetMessage(messageId);

            if (message == null)
                return NotFound();

            return Ok(message);
        }

        [HttpPost("{connectionId}")]
        public async Task<IActionResult> CreateMessage(int workerId, string connectionId, Message message)
        {
            if (workerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            message.SenderId = workerId;
            _repo.Add<Message>(message);
            if (await _repo.SaveAll())
            {
                await mHubContext.Clients.Clients(connectionId).MessageSended();
                return CreatedAtRoute("GetMessage", new { id = message.Id }, message);
            }
            return BadRequest("Wystąpiły błedy");
        }


        [HttpGet]
        public async Task<IActionResult> GetFirstSentence([FromQuery] PageParameters pageParameters, int workerId)
        {
            if (workerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();


            var messages = await _repo.getFirstSentences(pageParameters, workerId);

            Pagger<MessageFirstSentenceDto> messagesToReturn = new Pagger<MessageFirstSentenceDto>(messages);

            return Ok(messagesToReturn);
        }

        [HttpGet("conversation/{id}", Name = "GetConversation")]
        public async Task<IActionResult> GetConversation([FromQuery] PageParameters pageParameters, int workerId, int id)
        {
            if (workerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();



            var messages = await _repo.GetConversation(pageParameters, workerId, id);
            Pagger<MessageDisplayDto> messagesToReturn = new Pagger<MessageDisplayDto>(messages);

            return Ok(messagesToReturn);
        }


        [HttpPost("read/{id}")]
        public async Task<IActionResult> ReadMessage(int workerId, int id)
        {
            if (workerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var message = await _repo.GetMessage(id);

            if (message.RecipientId != workerId)
                return Unauthorized();

            message.IsRead = true;
            await _repo.SaveAll();

            return Ok();
        }

        [HttpGet("anyMessages")]
        public async Task<IActionResult> AnyMessages(int workerId)
        {

            if (workerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            int numOfMess = await _repo.CountMessagges(workerId);

            return Ok(numOfMess);
        }

    }
}