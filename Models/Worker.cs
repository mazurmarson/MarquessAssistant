namespace MarqueesAssistant.API.Controllers
{
    public class Worker
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }

        public string Rank { get; set; }

        public byte[]  PasswordHash { get; set; }   

        public byte[] PasswordSalt { get; set; }

    }
}