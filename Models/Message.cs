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
        public Worker Recipient { get; set;}
        public int RecipientId { get; set; }
        [Required(ErrorMessage = "Data wysłania wiadomości jest wymagana")]
        public DateTime SendDate { get; set; } 
        [Required(ErrorMessage = "Treść wiadomości jest wymagana")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Status wiadomości jest wymagana")]
        public bool IsRead { get; set; }
        public Message()
        {
            SendDate = DateTime.Now;
            IsRead = false;
        }
    }
}