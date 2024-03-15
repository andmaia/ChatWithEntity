using Application.Domain.Entity;

namespace Application.Domain.Repository
{
    public interface ITalkToUserRepository
    {
        Task<TalkToUser> Create(TalkToUser user);
        Task<TalkToUser> GetById(string id);
        Task<TalkToUser> Update(TalkToUser user);
        Task<IEnumerable<TalkToUser>> GetAllByUser(string id);
        Task<IEnumerable<TalkToUser>> GetAllByTalk(string id);

        Task<TalkToUser> GetByUserAndTalk(string userId,string talkId);

    }
}
