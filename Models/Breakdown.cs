using System;

namespace MarqueesAssistant.API.Models
{
    public class Breakdown
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }

        public string Description{ get; set; }

        public DateTime AccitdentDate { get; set;}

        public DateTime RepairdDate { get; set; }




    }
}