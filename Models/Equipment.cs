using System.ComponentModel.DataAnnotations;

namespace MarqueesAssistant.API.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Typ zasobu jest wymagany")]
        public string EquipmentType { get; set;}
        [Required(ErrorMessage = "Nazwa zasobu jest wymagana")]
        public string Name { get; set; }
    }


}