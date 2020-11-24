namespace MarqueesAssistant.API.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public Typ Typ { get; set;}

        public string Number { get; set; }
    }

    public enum Typ
    {
        vehicle,
        lifter,
        roof
    }
}