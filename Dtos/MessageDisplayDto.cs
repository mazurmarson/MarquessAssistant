using System;
using MarqueesAssistant.API.Controllers;
using MarqueesAssistant.API.Models;

namespace MarqueesAssistant.API.Dtos
{
    public class MessageDisplayDto
    {
        public  MessageDisplayDto() {}
        public int Id { get; set; }
        public int SenderId { get; set;}
        public int? RecipientId { get; set; }
        public DateTime SendDate { get; set; } 
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public string RecipientName {get; set;}
         public string SenderName {get; set;}
       public  MessageDisplayDto(Message message)
        {
         Id = message.Id;
         SenderId = message.SenderId;
         RecipientId = message.RecipientId;
         Content = message.Content;
         IsRead = message.IsRead;
         SendDate = message.SendDate;
         RecipientName = message.Recipient.Login;
        SenderName = message.Sender.Login;

        }
    }
}