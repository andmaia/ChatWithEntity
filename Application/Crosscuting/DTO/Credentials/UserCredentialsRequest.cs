namespace Application.Crosscuting.DTO.Credentials
{
    public class UserCredentialsRequest
    {
        public UserCredentialsRequest(string username,string email, string password, string passwordConfirmation)
        {
            Email = email;
            Password = password;
            PasswordConfirmation = passwordConfirmation;
            Username = username;
        }
        
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }


    }
}
