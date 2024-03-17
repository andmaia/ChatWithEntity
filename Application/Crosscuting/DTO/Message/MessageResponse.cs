using Application.Domain.Entity;

namespace Application.Crosscuting.DTO.Message
{
    public class MessageResponse
    {
        public MessageResponse()
        {
        }

        public MessageResponse(string id, string? text, DateTime dateCreate, string userId, string talkId)
        {
            Id = id;
            Text = text;
            DateCreate = dateCreate;
            UserId = userId;
            TalkId = talkId;
        }

        public string Id { get; set; }
        public string? Text { get; set; }
        public DateTime DateCreate { get; set; }
        public string UserId { get; set; }

        public string TalkId { get; set; }
    }
}
