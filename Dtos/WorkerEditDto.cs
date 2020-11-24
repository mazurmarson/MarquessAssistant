using System.ComponentModel.DataAnnotations;

namespace MarqueesAssistant.API.Dtos
{
    public class WorkerEditDto
    {
        public int Id { get; set;}
        [Required(ErrorMessage = "Login pracownika jest wymagany")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Haslo jest wymagane")]
        [StringLength(12, MinimumLength=6, ErrorMessage="Haslo musi składać z 6 do 12 znaków")]
        public string Password { get; set; }
        public string LastName { get; set; }

        public string FirstName { get; set;}
        public string Rank { get; set; }
    }
}