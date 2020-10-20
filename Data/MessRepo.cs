using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MarqueesAssistant.API.Data
{
    public class MessRepo : GenRepo, IMessRepo
    {
        private readonly DataContext _context;
        public MessRepo(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountMessagges(int workerId)
        {
            int numOfMess = await _context.Messages.Where(x => x.RecipientId == workerId && x.IsRead == false).CountAsync();
            return numOfMess;
        }

        public async Task<IEnumerable<MessageDisplayDto>> GetConversation(int workerId, int id)
        {
             var messages = await _context.Messages.Where(x =>
            (x.SenderId == workerId || x.RecipientId == workerId ) && 
            ( x.SenderId == id || x.RecipientId == id ) ).OrderByDescending(x => x.SendDate)
            .Include(Message => Message.Recipient)
            .Include(Message => Message.Sender)
            .Select(Message => new MessageDisplayDto(Message)).ToListAsync();

            return messages;
        }

        public List<Message> getFirstSentences(int workerId)
        {
                        List<Message> MessagesList = new List<Message>();
            // var users = 
            int id =  _context.Workers.Select(  u => u.Id).Max();
            

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
            
            return  MessagesList;
        }

        public async Task<Message> GetMessage(int messageId)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(x => x.Id == messageId);
            return message;    
        }    
    }
}