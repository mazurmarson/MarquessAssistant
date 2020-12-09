using System.Threading.Tasks;

namespace MarqueesAssistant.API.signalR
{
    public interface IMessageHubClient
    {
         Task BroadcastMessage(string message);
    }
}