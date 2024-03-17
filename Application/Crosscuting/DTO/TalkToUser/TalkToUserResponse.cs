using Application.Crosscuting.DTO.Message;
using Application.Domain.Entity;

namespace Application.Crosscuting.DTO.TalkToUser
{
    public class TalkToUserResponse
    {
        private string id1;
        private string id2;

        public TalkToUserResponse()
        {
        }

        public TalkToUserResponse(string id1, DateTime dataCreated, string idUser, string id2, bool isArchived)
        {
            this.id1 = id1;
            DataCreated = dataCreated;
            IdUser = idUser;
            this.id2 = id2;
            IsArchived = isArchived;
        }

        public TalkToUserResponse(string id, DateTime dataCreated, string idUser, string username, string idTalk, bool isArchived)
        {
            Id = id;
            DataCreated = dataCreated;
            IdUser = idUser;
            Username = username;
            IdTalk = idTalk;
            IsArchived = isArchived;
        }

        public string? Id { get; set; }
        public DateTime DataCreated { get; set; }
        public string? IdUser { get; set; }
        public string? Username { get; set; }   
        public string? IdTalk { get; set; }
        public bool? IsArchived { get; set; }

    }
}
