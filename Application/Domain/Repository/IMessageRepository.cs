using Application.Domain.Entity;

namespace Application.Domain.Repository
{
    public interface IMessageRepository
    {
        Task<bool> Create(Message data);
        Task<Message> GetById (string  id);
        Task<IEnumerable<Message>> GetAllbyTalkAndUser(string talkId,string userId);
        Task<IEnumerable<Message>> GetAllbyTalk(string talkId);
        Task<IEnumerable<Message>> GetAllbyTalkToUserId(string userId);
        Task<Message> GetLastMessage(string idTalk);
    }
}
