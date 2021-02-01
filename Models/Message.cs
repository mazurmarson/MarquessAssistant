using System;
using System.ComponentModel.DataAnnotations;
using MarqueesAssistant.API.Controllers;

namespace MarqueesAssistant.API.Models
{
    public class Message
    {
        public int Id { get; set; }
        public Worker Sender { get; set; }
        public int SenderId { get; set;}
        public Worker? Recipient { get; set;}
        public int? RecipientId { get; set; }
        public DateTime SendDate { get; set; } 
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public Message()
        {
            SendDate = DateTime.Now;
            IsRead = false;
        }
    }
}