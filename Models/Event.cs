using System;

namespace MarqueesAssistant.API.Models
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Place { get; set; }

        public string TypeOfEvent { get; set; }
        
    }
}