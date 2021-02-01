using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MarqueesAssistant.API.Models;

namespace MarqueesAssistant.API.Controllers
{
    public class Worker
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Login jest wymagany")]
        public string Login { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Ranga pracownika jest wymagana")]
        public string Rank { get; set; }
        public byte[]  PasswordHash { get; set; }   
        public byte[] PasswordSalt { get; set; }
        public ICollection<Message> MessagesSent { get; set;}
        public ICollection<Message> MessagesRecived { get; set;}

    }
}