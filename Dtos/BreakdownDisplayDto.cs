using System;

namespace MarqueesAssistant.API.Dtos
{
    public class BreakdownDisplayDto
    {
        public int Id { get; set; }
       
        

        public string Description{ get; set; }

        public DateTime? AccitdentDate { get; set;}

        public DateTime RepairdDate { get; set; }

        public string EquipmentName  { get; set;} 
    }
}