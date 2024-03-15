using Application.Crosscuting.DTO.Talk;
using Application.Crosscuting.Helpers;
using Application.Domain.Entity;

namespace Application.Domain.Service
{
    public interface ITalkService
    {
        Task<ServiceResult<TalkResponse>> GetTalkById(string id);
        Task<ServiceResult<IEnumerable<TalkResponse>>> GetTallByUserId(string id);
    }
}
