using Application.Domain.Entity;

namespace Application.Crosscuting.DTO.Message
{
    public class MessageRequest
    {
      

        public string? Text { get; set; }
        public string? UserId { get; set; }

        public string? TalkId { get; set; }
    }
}
