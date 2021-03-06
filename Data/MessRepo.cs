using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Helpers;
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
            int numOfMess = await _context.Messages.Where
            (x => x.RecipientId == workerId && x.IsRead == false).CountAsync();
            return numOfMess;
        }

        public async Task<PagedList<MessageDisplayDto>> GetConversation(
            PageParameters pageParameters, int workerId, int id)
        {
            List<MessageDisplayDto> messages = await _context.Messages.Where(x =>
           (x.SenderId == workerId || x.RecipientId == workerId) &&
           (x.SenderId == id || x.RecipientId == id)).OrderByDescending(x => x.SendDate)
           .Include(Message => Message.Recipient)
           .Include(Message => Message.Sender)
           .Select(Message => new MessageDisplayDto(Message)).ToListAsync();



            return PagedList<MessageDisplayDto>
            .ToPagedList(messages, pageParameters.PageNumber, pageParameters.PageSize);
        }

        public async Task<PagedList<MessageFirstSentenceDto>> getFirstSentences(
            PageParameters pageParameters, int workerId)
        {
            List<MessageFirstSentenceDto> MessagesList = 
            new List<MessageFirstSentenceDto>();

            int id = _context.Workers.Select(u => u.Id).Max();
            for (int i = 0; i <= id; i++)
            {
                if (workerId != i)
                {
                    var messages = await _context.Messages.Where(x =>
                     (x.SenderId == workerId || x.RecipientId == workerId) &&
                    (x.SenderId == i || x.RecipientId == i)).OrderByDescending(x => x.SendDate)
                    .Take(1).ToListAsync();
                    var message = messages.FirstOrDefault();


                    string userName = _context.Workers.Where(x => x.Id == i).Select(x => x.Login).FirstOrDefault();

                    var MessageWithUserName = new MessageFirstSentenceDto()
                    {
                        Message = message,
                        NameOfUser = userName
                    };
                    if (message != null)
                        MessagesList.Add(MessageWithUserName);
                }
            }

            return PagedList<MessageFirstSentenceDto>.ToPagedList(
                MessagesList, pageParameters.PageNumber, pageParameters.PageSize);

        }

        public async Task<Message> GetMessage(int messageId)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(x => x.Id == messageId);
            return message;
        }
    }
}