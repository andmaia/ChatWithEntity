namespace Application.Crosscuting.DTO.User
{
    public class UserResponse
    {
        public UserResponse()
        {
        }

        public UserResponse(string id, string name, DateTime dataCreated, bool isActive, byte[] photoUser)
        {
            Id = id;
            Name = name;
            DataCreated = dataCreated;
            IsActive = isActive;
            PhotoUser = photoUser;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DataCreated { get; set; }
        public bool IsActive { get; set; }
        public byte[] PhotoUser { get; set; }
    }
}
