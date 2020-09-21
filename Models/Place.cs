namespace MarqueesAssistant.API.Models
{
    public class Place
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }

        public string FirstGradeDivision { get; set; } // Województwo

        public string SecondGradeDivision { get; set; } // Powiat

        public string ThirdGradeDivision { get; set; } // Gmina

        public string PostCode { get; set; }

    }
}