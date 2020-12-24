using System;
using System.ComponentModel.DataAnnotations;

namespace MarqueesAssistant.API.Models
{
    public class Breakdown
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
        [Required(ErrorMessage = "Opis jest wymagany")]
        public string Description{ get; set; }
        [Required(ErrorMessage = "Data zdarzenia jest wymagana")]
        public DateTime? AccitdentDate { get; set;}

        public DateTime RepairdDate { get; set; }




    }
}