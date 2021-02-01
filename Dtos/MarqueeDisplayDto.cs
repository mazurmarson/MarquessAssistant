using System;

namespace MarqueesAssistant.API.Dtos
{
    public class MarqueeDisplayDto
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public int? Width { get; set; }
        public int? Length { get; set; }
        public string Description { get; set; }
        public bool? IsUp { get; set; }
        public bool? IsDown { get; set; }
    }
}