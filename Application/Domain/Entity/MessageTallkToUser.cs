namespace Application.Domain.Entity
{
    public class MessageTallkToUser
    {
        public MessageTallkToUser()
        {
        }

        public MessageTallkToUser(string id, string talkToUserId, string messageId)
        {
            Id = id;
            TalkToUserId = talkToUserId;
            MessageId = messageId;
        }

        public string Id { get; set; }
        public string TalkToUserId {  get; set; }

        public TalkToUser TalkToUser { get; set; }
        public Message Message { get; set; }    
        public string MessageId {  get; set; }
    }
}
