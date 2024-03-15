namespace Application.Domain.Entity
{
    public class Message
    {
        public Message(string id, string? text, DateTime dateCreate, DateTime dateUpdate, DateTime dateFinished, bool isActive, byte[]? file, string userId, string talkId, string talkToUserId)
        {
            Id = id;
            Text = text;
            DateCreate = dateCreate;
            DateUpdate = dateUpdate;
            DateFinished = dateFinished;
            IsActive = isActive;
            File = file;
            UserId = userId;
            TalkId = talkId;
            MessageTallkToUsers = new List<MessageTallkToUser>();
            
        }

        public string Id { get; set; }
        public string? Text { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        public DateTime DateFinished { get; set; }
        public bool IsActive { get; set; } = true;
        public byte[]? File { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }

        public Task Talk { get; set; }
        public string TalkId { get; set; }
        
        public IEnumerable<MessageTallkToUser> MessageTallkToUsers { get; set; }

    }
}
