using System.Collections.Generic;
using System.Threading.Tasks;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Helpers;
using MarqueesAssistant.API.Models;

namespace MarqueesAssistant.API.Data
{
    public interface IMessRepo : IGenRepo
    {
         Task<Message> GetMessage(int messageId);
         Task<PagedList<MessageDisplayDto>> GetConversation(PageParameters pageParameters,int workerId, int id);
         Task<int> CountMessagges(int workerId);

         List<MessageFirstSentenceDto> getFirstSentences(int workerId);
    }
}