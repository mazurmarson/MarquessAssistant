using System;

namespace MarqueesAssistant.API.Dtos
{
    public class EventDisplayDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int PlaceId { get; set; }

        public string PlaceName { get; set; }

        public string TypeOfEvent { get; set; }
    }
}