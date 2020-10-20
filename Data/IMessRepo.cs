using System.Collections.Generic;
using System.Threading.Tasks;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Models;

namespace MarqueesAssistant.API.Data
{
    public interface IMessRepo : IGenRepo
    {
         Task<Message> GetMessage(int messageId);
         Task<IEnumerable<MessageDisplayDto>> GetConversation(int workerId, int id);
         Task<int> CountMessagges(int workerId);

         List<Message> getFirstSentences(int workerId);
    }
}