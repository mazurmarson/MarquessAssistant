using System;
using System.ComponentModel.DataAnnotations;

namespace MarqueesAssistant.API.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Data początku jest wymagana")]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "Data końca jest wymagana")]
        public DateTime? EndDate { get; set; }
        public int PlaceId { get; set; }
        public Place Place { get; set; }
        [Required(ErrorMessage = "Typ imprezy jest wymagany")]
        public string TypeOfEvent { get; set; }


    }



    
}