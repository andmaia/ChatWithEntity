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

        public TalkService(ITalkRepository talkRepository, IUserRepository userRepository)
        {
            _talkRepository = talkRepository;
            _userRepository = userRepository;
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
                TalkToUserResponses = talk.TalkToUsers.Select(tt => new TalkToUserResponse(tt.Id, tt.DataCreated, tt.IdUser, tt.User.IdentityUser.UserName, tt.IdTalk, tt.IsArchived))
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

            var talks = await _talkRepository.GetTalksByUserId(user.Id);
            if (talks == null || !talks.Any())
            {
                return new ServiceResult<IEnumerable<TalkResponse>>
                {
                    Success = true,
                    Data = Enumerable.Empty<TalkResponse>(),
                };
            }

            var talkResponses = talks.Select(talk => new TalkResponse
            {
                Id = talk.Id,
                DataCreated = talk.DataCreated,
                TalkToUserResponses = talk.TalkToUsers.Select(tt => new TalkToUserResponse(tt.Id, tt.DataCreated, tt.IdUser, tt.User.IdentityUser.UserName, tt.IdTalk, tt.IsArchived))
            }).ToList();

            return new ServiceResult<IEnumerable<TalkResponse>>
            {
                Success = true,
                Data = talkResponses,
            };
        }

    }
}
