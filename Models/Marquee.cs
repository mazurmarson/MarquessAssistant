using System;
using System.ComponentModel.DataAnnotations;

namespace MarqueesAssistant.API.Models
{
    public class Marquee
    {
        public int Id { get; set; }
        
        public Event Event { get; set; }

        public int EventId { get; set; }
        [Required(ErrorMessage = "Szerokość jest wymagana")]
        [DivisibleByThree( ErrorMessage = "Szerokość musi być podzielna przez 3")]
        public int? Width { get; set; }
        [Required(ErrorMessage = "Długość jest wymagana")]
        [DivisibleByThree( ErrorMessage = "Długość musi być podzielna przez 3")]
        public int? Length { get; set; }

        [Required(ErrorMessage = "Status wymagany")]
        public bool? IsUp { get; set; }
        [Required(ErrorMessage = "Status wymagany")]
        public bool? IsDown { get; set; }

        public string Description { get; set; }
    }

    public class DivisibleByThree : ValidationAttribute
    {
        

        
        public override bool IsValid(object value)
        {
            if(value==null)
            {
                return true;
            }
            if((int)value % 3 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}