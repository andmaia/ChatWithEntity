namespace Application.Crosscuting.DTO.Credentials
{
    public class UserLoginResponse
    {
        public string? Token { get; set; }
        public UserCredentialsResponse UserCredentialsResponse { get; set; }
    }
}
