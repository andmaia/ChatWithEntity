using Application.Crosscuting.DTO.TalkToUser;

namespace Application.Crosscuting.DTO.Talk
{
    public class TalkResponse
    {
        public string? Id { get; set; }
        public DateTime? DataCreated { get; set; }
        public IEnumerable<TalkToUserResponse>? TalkToUserResponses { get; set; }
    }
}
