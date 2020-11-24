using System;
using MarqueesAssistant.API.Models;

namespace MarqueesAssistant.API.Dtos
{
    public class MarqueesStuffDto
    {
        public MarqueesStuffDto() {}
        public int Id { get; set; }

        public string EventName  { get; set; }
        
        public Event Event { get; set; }

        public int EventId { get; set; }

        public int Width { get; set; }

        public int Length { get; set; }

        public DateTime UpDate { get; set; }

        public DateTime DownDate { get; set; }

        public bool IsUp { get; set; }

        public bool IsDown { get; set; }

        public MarqueesStuffDto(Marquee marquee)
        {
            Id = marquee.Id;
            EventName = marquee.Event.Name;
            EventId = marquee.EventId;
            Width = marquee.Width;
            Length = marquee.Length;
          //  UpDate = marquee.UpDate;
          //  DownDate = marquee.DownDate;
            IsUp = marquee.IsUp;
            IsDown = marquee.IsDown;

        }

    }
}