namespace Application.Crosscuting.DTO.User
{
    public class UserRequest
    {
        public UserRequest(string name, string idCredentials)
        {
            Name = name;
            IdCredentials = idCredentials;
        }

        public string Name { get; set; }

        public string IdCredentials { get; set; }

    }
}
