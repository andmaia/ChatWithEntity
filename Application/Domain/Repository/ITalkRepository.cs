using Application.Domain.Entity;

namespace Application.Domain.Repository
{
    public interface ITalkRepository
    {

        Task<Talk> GetTaslkById(string id);
        Task<Talk> CreateTalk(Talk data);

        Task<IEnumerable<Talk>> GetTalksByUserId(string id);

    }
}
