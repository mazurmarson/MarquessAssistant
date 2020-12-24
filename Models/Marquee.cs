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
        public int? Width { get; set; }
        [Required(ErrorMessage = "Długość jest wymagana")]
        public int? Length { get; set; }

      //  public DateTime UpDate { get; set; }

    // public DateTime DownDate { get; set; }
        [Required(ErrorMessage = "Status wymagany")]
        public bool? IsUp { get; set; }
        [Required(ErrorMessage = "Status wymagany")]
        public bool? IsDown { get; set; }

        public string Description { get; set; }
    }
}