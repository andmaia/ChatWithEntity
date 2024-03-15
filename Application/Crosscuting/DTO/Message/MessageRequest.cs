using Application.Domain.Entity;

namespace Application.Crosscuting.DTO.Message
{
    public class MessageRequest
    {
        public MessageRequest(string? text, byte[]? file, string userId, string talkId, string talkToUserId)
        {
            Text = text;
            File = file;
            UserId = userId;
            TalkId = talkId;
            TalkToUserId = talkToUserId;
        }

        public string? Text { get; set; }
        public byte[]? File { get; set; }
        public string UserId { get; set; }

        public string TalkId { get; set; }
        public string TalkToUserId { get; set; }
    }
}
