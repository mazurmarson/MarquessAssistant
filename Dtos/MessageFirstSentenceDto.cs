using MarqueesAssistant.API.Models;

namespace MarqueesAssistant.API.Dtos
{
    public class MessageFirstSentenceDto
    {
        public Message Message { get; set;}

        public string NameOfUser { get; set; }

    }
}