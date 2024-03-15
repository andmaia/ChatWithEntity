using Application.Crosscuting.DTO.Talk;
using Application.Crosscuting.DTO.TalkToUser;
using Application.Crosscuting.Helpers;

namespace Application.Domain.Service
{
    public interface ITalkToUserService
    {
        Task<ServiceResult<IEnumerable<TalkToUserResponse>>> CreateTalk (TalkRequest data);
        Task<ServiceResult<IEnumerable<TalkToUserResponse>>> GetAllByUser(string userId);
        Task<ServiceResult<IEnumerable<TalkToUserResponse>>> GetAllByTalk(string talkId);
        Task<ServiceResult<TalkToUserResponse>> GetAllByUserTalk(string talkId,string userId);
        Task<ServiceResult<TalkToUserResponse>> GetById(string id);
        Task<ServiceResult<bool>> ArchiveTalk(string id);
        Task<ServiceResult<bool>> FavoriteMessage(string messageId,string talkToUserId);



    }
}
