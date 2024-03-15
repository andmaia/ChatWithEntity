using Application.Domain.Entity;

namespace Application.Domain.Repository
{
    public interface IMessageTalkToUserRepository
    {
        Task<string> Create(MessageTallkToUser data);
        Task<MessageTallkToUser> GetById(string id);
        Task<IEnumerable<MessageTallkToUser>> GetByAllByTalkToUserId(string id);
        Task<IEnumerable<MessageTallkToUser>> GetByAllByMessageId(string id);

    }
}
