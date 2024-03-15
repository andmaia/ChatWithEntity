namespace Application.Domain.Entity
{
    public class TalkToUser
    {
        public TalkToUser()
        {
        }

        public TalkToUser(string id, DateTime dataCreated, string idUser, string idTalk, bool isArchived)
        {
            Id = id;
            DataCreated = dataCreated;
            IdUser = idUser;
            IdTalk = idTalk;
            IsArchived = isArchived;
            MessageTallkToUsers = new List<MessageTallkToUser>();
        }

        public string Id { get; set; }
        public DateTime DataCreated { get; set; }
        public string IdUser { get; set; }
        public User User { get; set; }
        public string IdTalk { get; set; }
        public Talk Talk { get; set; }
        public bool IsArchived { get; set; }

        public IEnumerable<MessageTallkToUser> MessageTallkToUsers { get; set; }

    }
}
