using System.ComponentModel.DataAnnotations;

namespace MarqueesAssistant.API.Dtos
{
    public class WorkerRegisterDto
    {
        [Required(ErrorMessage = "Login pracownika jest wymagany")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Haslo jest wymagane")]
        [StringLength(12, MinimumLength=6, ErrorMessage="Haslo musi składać z 6 do 12 znaków")]
        public string Password { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set;}

        public string FirstName { get; set;}
         [Required(ErrorMessage = "Ranga użytkownika jest wymagana")]
        public string Rank { get; set; }
    }
}