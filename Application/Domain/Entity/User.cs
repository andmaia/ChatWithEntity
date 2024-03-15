using Microsoft.AspNetCore.Identity;

namespace Application.Domain.Entity
{
    public class User
    {
        public User()
        {
        }

        public User(string id, string name, DateTime dataCreated, DateTime dateUpdated, DateTime dateFinished, bool isActive, byte[] photoUser, string identityUserId)
        {
            Id = id;
            Name = name;
            DataCreated = dataCreated;
            DateUpdated = dateUpdated;
            DateFinished = dateFinished;
            IsActive = isActive;
            PhotoUser = photoUser;
            IdentityUserId = identityUserId;
            Messages = new List<Message>();
            TalkToUsers = new List<TalkToUser>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DataCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateFinished { get; set; }
        public bool IsActive { get; set; }
        public byte[]? PhotoUser { get; set; }

        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<TalkToUser> TalkToUsers { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public string IdentityUserId { get; set; }
    }
}
