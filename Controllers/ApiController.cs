using MarqueesAssistant.API.signalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MarqueesAssistant.API.Controllers
{
    public class ApiController : ControllerBase
    {
        private readonly IHubContext<MessageHub, IMessageHubClient> mHubContext;

        public ApiController(IHubContext<MessageHub, IMessageHubClient> hubContext)
        {
            mHubContext = hubContext;
        } 

        [HttpGet]
        [Route("send")]
        public void SendMessage(string message)
        {
        //     mHubContext.Clients.All.BroadcastMessage(message);
        }

    }
}