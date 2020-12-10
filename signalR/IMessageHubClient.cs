using System.Collections.Generic;
using System.Threading.Tasks;
using MarqueesAssistant.API.Dtos;

namespace MarqueesAssistant.API.signalR
{
    public interface IMessageHubClient
    {
         Task BroadcastMessage(string message);
         Task GetConversation(IEnumerable<MessageDisplayDto> messages);
         
    }
}