using Application.Crosscuting.DTO.Message;
using Application.Crosscuting.Helpers;

namespace Application.Domain.Service
{
    public interface IMessageService
    {
        Task<ServiceResult<MessageResponse>> CreateMessage(MessageRequest data);
        Task<ServiceResult<MessageResponse>> GetById(string id);
        Task<ServiceResult<IEnumerable<MessageResponse>>> GetAllbyTalkAndUser(string talkId, string userId);
        Task<ServiceResult<IEnumerable<MessageResponse>>> GetAllbyTalk(string talkId);
        Task<ServiceResult<IEnumerable<MessageResponse>>> GetAllbyTalkToUserId(string userId);
    }
}
