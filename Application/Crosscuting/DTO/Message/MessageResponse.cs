using Application.Domain.Entity;

namespace Application.Crosscuting.DTO.Message
{
    public class MessageResponse
    {
        public MessageResponse(string id, string? text, DateTime dateCreate, bool isActive, string userId, string talkId, string talkToUserId)
        {
            Id = id;
            Text = text;
            DateCreate = dateCreate;
            IsActive = isActive;
            UserId = userId;
            TalkId = talkId;
            TalkToUserId = talkToUserId;
        }

        public string Id { get; set; }
        public string? Text { get; set; }
        public DateTime DateCreate { get; set; }
        public bool IsActive { get; set; } = true;
        public string UserId { get; set; }

        public string TalkId { get; set; }
        public string TalkToUserId { get; set; }
    }
}
