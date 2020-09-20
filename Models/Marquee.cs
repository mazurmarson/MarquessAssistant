using System;

namespace MarqueesAssistant.API.Models
{
    public class Marquee
    {
        public int Id { get; set; }
        public string Event { get; set; }

        public int Width { get; set; }

        public int Length { get; set; }

        public DateTime UpDate { get; set; }

        public DateTime DownDate { get; set; }

        public bool IsUp { get; set; }

        public bool IsDown { get; set; }
    }
}