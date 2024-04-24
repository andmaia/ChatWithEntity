using Application.Crosscuting.DTO.Message;
using Application.Crosscuting.DTO.Talk;
using Application.Crosscuting.DTO.TalkToUser;
using Application.Crosscuting.Helpers;
using Application.Domain.Repository;
using Application.Domain.Service;

namespace Application.Service
{
    public class TalkService : ITalkService
    {
        private readonly ITalkRepository _talkRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;

        public TalkService(ITalkRepository talkRepository, IUserRepository userRepository, IMessageRepository messageRepository)
        {
            _talkRepository = talkRepository;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        public async Task<ServiceResult<TalkResponse>> GetTalkById(string id)
        {
            var talk =await _talkRepository.GetTaslkById(id);
            if(talk is null)
            {
                return new()
                {
                    Success = false,
                    MessageError = $"Talk with id: {id} not exists",
                };
            }

            TalkResponse talkRespons = new()
            {
                Id = talk.Id,
                DataCreated = talk.DataCreated,
                TalkToUserResponses = talk.TalkToUsers.Select(tt => new TalkToUserResponse() { Id = tt.Id, DataCreated = tt.DataCreated, IdTalk = tt.IdTalk, IdUser = tt.IdUser, IsArchived = tt.IsArchived, Username = tt.User.Name })
            };

            return new()
            {
                Success = true,
                Data = talkRespons
            };

        }
        public async Task<ServiceResult<IEnumerable<TalkResponse>>> GetTallByUserId(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return new ServiceResult<IEnumerable<TalkResponse>>
                {
                    Success = false,
                    MessageError = $"User with id: {id} does not exist",
                };
            }

            var talks = await _talkRepository.GetTalksByUserId(id);
            if (talks == null || !talks.Any())
            {
                return new ServiceResult<IEnumerable<TalkResponse>>
                {
                    Success = true,
                    Data = Enumerable.Empty<TalkResponse>(),
                };
            }


            var talkResponses = new List<TalkResponse>();

            foreach (var talk in talks)
            {
                var lastMessage = await _messageRepository.GetLastMessage(talk.Id);
                var lastMessageResponse = lastMessage != null ? new MessageResponse(lastMessage.Id, lastMessage.Text, lastMessage.DateCreate, lastMessage.UserId, lastMessage.TalkId) : null;

                var talkResponse = new TalkResponse
                {
                    Id = talk.Id,
                    DataCreated = talk.DataCreated,
                    TalkToUserResponses = talk.TalkToUsers.Select(tt => new TalkToUserResponse
                    {
                        Id = tt.Id,
                        DataCreated = tt.DataCreated,
                        IdTalk = tt.IdTalk,
                        IdUser = tt.IdUser,
                        IsArchived = tt.IsArchived,
                        Username = tt.User.Name,
                        LastMessage = lastMessageResponse
                    }).ToList()
                };

                talkResponses.Add(talkResponse);
            }

            return new ServiceResult<IEnumerable<TalkResponse>>
            {
                Success = true,
                Data = talkResponses,
            };
        }

    }
}
