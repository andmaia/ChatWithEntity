namespace Application.Crosscuting.DTO.Credentials
{
    public class UserCredentialsLogin
    {
        public UserCredentialsLogin(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }

    }
}
