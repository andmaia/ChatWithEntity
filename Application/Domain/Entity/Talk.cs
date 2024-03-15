namespace Application.Domain.Entity
{
    public class Talk
    {
        public Talk()
        {
        }

        public Talk(string id, DateTime dataCreated)
        {
            Id = id;
            DataCreated = dataCreated;
            Messages = new List<Message>();
            TalkToUsers = new List<TalkToUser>();
        }

        public string Id { get; set; }
        public DateTime DataCreated { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<TalkToUser> TalkToUsers { get; set; }
    }
}
