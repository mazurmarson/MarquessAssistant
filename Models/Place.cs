using System.ComponentModel.DataAnnotations;

namespace MarqueesAssistant.API.Models
{
    public class Place
    {
        public int Id { get; set; }

        public int Number { get; set; }
        public string Street { get; set; }
    
        [Required(ErrorMessage = "Miejscowosc jest wymagana")]
        public string Town { get; set; }
        [Required(ErrorMessage = "Województwo jest wymagane")]
        public string FirstGradeDivision { get; set; } // Województwo

        public string SecondGradeDivision { get; set; } // Powiat

        public string ThirdGradeDivision { get; set; } // Gmina

        public string PostCode { get; set; }

    }
}