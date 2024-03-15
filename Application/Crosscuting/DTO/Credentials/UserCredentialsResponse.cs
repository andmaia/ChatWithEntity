namespace Application.Crosscuting.DTO.Credentials
{
    public class UserCredentialsResponse
    {
        public UserCredentialsResponse(string id, string email)
        {
            Id = id;
            Email = email;
        }

        public string Id { get; set; }
        public string Email { get; set; }


    }
}
