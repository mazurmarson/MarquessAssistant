using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarqueesAssistant.API.Dtos;

namespace MarqueesAssistant.API.signalR
{
    public interface IMessageHubClient
    {
         Task MessageSended();
         
         
    }
}