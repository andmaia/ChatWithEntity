namespace Application.Crosscuting.DTO.Credentials
{
    public class UserCredentialsUpdate
    {
        public UserCredentialsUpdate(string id, string email, string password, string passwordConfirmation, string newPassword)
        {
            Id = id;
            Email = email;
            Password = password;
            PasswordConfirmation = passwordConfirmation;
            NewPassword = newPassword;
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string NewPassword { get; set; }


    }
}
