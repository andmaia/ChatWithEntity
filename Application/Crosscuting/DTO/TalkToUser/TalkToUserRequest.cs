namespace Application.Crosscuting.DTO.TalkToUser
{
    public class TalkToUserRequest
    {
        public TalkToUserRequest(string idUser, string idTalk)
        {
            IdUser = idUser;
            IdTalk = idTalk;
        }

        public string IdUser { get; set; }
        public string IdTalk { get; set; }
    }
}
