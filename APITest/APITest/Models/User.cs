namespace APITest.Models
{
    public class User
    {
        public Guid id { get; init; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
