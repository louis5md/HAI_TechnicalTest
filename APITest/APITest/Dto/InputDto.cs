namespace APITest.Dto
{
    public class RegistrationDto
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }

    public class LoginDto
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class UpdateUserDto
    {
        public string name { get; set; }
        public string password { get; set; }
    }

}
