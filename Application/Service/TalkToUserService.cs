using Application.Crosscuting.DTO.Talk;
using Application.Crosscuting.DTO.TalkToUser;
using Application.Crosscuting.Helpers;
using Application.Domain.Entity;
using Application.Domain.Repository;
using Application.Domain.Service;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Service
{
    public class TalkToUserService : ITalkToUserService
    {

        private readonly ITalkToUserRepository _talkToUserRepository;
        private readonly ITalkRepository _talkRepository;
        private readonly IUserRepository _userRepository;   
        private readonly IMessageRepository _messageRepository;
        private readonly IMessageTalkToUserRepository _messageTalkToUserRepository;

        public TalkToUserService(ITalkToUserRepository talkToUserRepository, ITalkRepository talkRepository, IUserRepository userRepository, IMessageRepository messageRepository, IMessageTalkToUserRepository messageTalkToUserRepository)
        {
            _talkToUserRepository = talkToUserRepository;
            _talkRepository = talkRepository;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _messageTalkToUserRepository = messageTalkToUserRepository;
        }

        public async Task<ServiceResult<bool>> ArchiveTalk(string id)
        {
            var talkToUser = await _talkToUserRepository.GetById(id);
            if(talkToUser == null)
            {
                return new()
                {
                    Success = false,
                    MessageError = $"Object talkToUser with id:{id} no exists"
                };
            }

            talkToUser.IsArchived = !talkToUser.IsArchived;
            var result =await _talkToUserRepository.Update(talkToUser);

            return new()
            {
                Success = result != null,
                MessageError = result != null ?string.Empty:"Fail to archived talk"
            };

        }

        public async Task<ServiceResult<IEnumerable<TalkToUserResponse>>> CreateTalk(TalkRequest data)
        {
            var userBegin = await _userRepository.GetUserById(data.IdBegin);

            if (userBegin == null)
            {
                return new()
                {
                    Success = false,
                    MessageError = $"User with id {data.IdBegin} not exists"
                };
            }
            var userEnd = await _userRepository.GetUserById(data.IdEnd);
            if (userEnd == null)
            {
                return new()
                {
                    Success = false,
                    MessageError = $"User with id {data.IdEnd} not exists"
                };
            }

            Talk talk = new()
            {
                Id = Guid.NewGuid().ToString(),
                DataCreated = DateTime.Now,
            };

            var resultTalk=await _talkRepository.CreateTalk(talk);

            var takToUserBegin = new TalkToUser()
            {
                Id = Guid.NewGuid().ToString(),
                DataCreated = DateTime.Now,
                IdTalk = resultTalk.Id,
                Talk = talk,
                User = userBegin,
                IdUser = userBegin.Id,
                IsArchived = false,
            };

            var takToUserEnd = new TalkToUser()
            {
                Id = Guid.NewGuid().ToString(),
                DataCreated = DateTime.Now,
                IdTalk = resultTalk.Id,
                Talk = talk,
                User = userEnd,
                IdUser = userEnd.Id,
                IsArchived = false,
            };

            var resultUserBegin =await _talkToUserRepository.Create(takToUserBegin);
            var resultUserEnd = await _talkToUserRepository.Create(takToUserEnd);

            if(resultUserBegin == null || resultUserEnd == null)
            {
                return new()
                {
                    Success = false,
                    MessageError = "Fail to create talk."
                };
            }

            var listToResponse = new List<TalkToUserResponse>
            {
                new TalkToUserResponse()
                {
                    Id = resultUserBegin.Id,
                    DataCreated = resultUserBegin.DataCreated,
                    IdTalk =resultUserBegin.IdTalk,
                    IdUser = resultUserBegin.IdUser,
                    IsArchived=resultUserBegin.IsArchived,
                    Username=resultUserBegin.User.Name
                },
                new TalkToUserResponse()
                {
                    Id = resultUserEnd.Id,
                    DataCreated = resultUserEnd.DataCreated,
                    IdTalk =resultUserEnd.IdTalk,
                    IdUser = resultUserEnd.IdUser,
                    IsArchived=resultUserEnd.IsArchived,
                    Username=resultUserEnd.User.Name
                }

            };

            return new()
            {
                Success = true,
                Data = listToResponse
            };

        }

        public async Task<ServiceResult<bool>> FavoriteMessage(string messageId, string talkToUserId)
        {
            var talkToUser = await _talkToUserRepository.GetById(talkToUserId);
            if (talkToUser == null)
            {
                return new()
                {
                    Success = false,
                    MessageError = $"Object talkToUser with id:{talkToUserId} no exists"
                };
            }

            var message= await _messageRepository.GetById(messageId);
            if (message == null)
            {
                return new()
                {
                    Success = false,
                    MessageError = $"Object message with id:{messageId} no exists"
                };
            }

            var messageToTalkToUser = new MessageTallkToUser()
            {
                Id = Guid.NewGuid().ToString(),
                MessageId = messageId,
                TalkToUserId = talkToUserId,
                Message = message,
                TalkToUser = talkToUser
            };

            _ = _messageTalkToUserRepository.Create(messageToTalkToUser);

            talkToUser.MessageTallkToUsers.ToList().Add(messageToTalkToUser);
            _ = _talkToUserRepository.Update(talkToUser);

            return new()
            {
                Success = true,
            };

        }

        public  async Task<ServiceResult<IEnumerable<TalkToUserResponse>>> GetAllByTalk(string talkId)
        {
            var talk = await _talkRepository.GetTaslkById(talkId);
            if (talk == null)
            {
                return new()
                {
                    Success = false,
                    MessageError = $"Object talk with id:{talk} no exists"
                };
            }

            var talkToUsers =await _talkToUserRepository.GetAllByTalk(talkId);
            if (talkToUsers== null || !talkToUsers.Any())
            {
                return new()
                {
                    Success = true,
                    Data = Enumerable.Empty<TalkToUserResponse>(),
                };
            }

            var talkResponses = talkToUsers.Select(tt =>
              new TalkToUserResponse()
              {
                  Id = tt.Id,
                  DataCreated = tt.DataCreated,
                  IdTalk = tt.IdTalk,
                  IdUser = tt.IdUser,
                  IsArchived = tt.IsArchived,
                  Username = tt.User.Name
              }).ToList();
            return new()
            {
                Success = true,
                Data = talkResponses
            };
        }

        public async Task<ServiceResult<IEnumerable<TalkToUserResponse>>> GetAllByUser(string userId)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null)
            {
                return new()
                {
                    Success = false,
                    MessageError = $"User with id {userId} not exists"
                };
            };

            var talkToUsers = await _talkToUserRepository.GetAllByUser(userId);
            if (talkToUsers == null || !talkToUsers.Any())
            {
                return new()
                {
                    Success = true,
                    Data = Enumerable.Empty<TalkToUserResponse>(),
                };
            }

            var talkResponses = talkToUsers.Select(tt =>
            new TalkToUserResponse()
            {
                Id = tt.Id,
                DataCreated = tt.DataCreated,
                IdTalk = tt.IdTalk,
                IdUser = tt.IdUser,
                IsArchived = tt.IsArchived,
                Username = tt.User.Name
            }).ToList();
            return new()
            {
                Success = true,
                Data = talkResponses
            };
       }

        public  async Task<ServiceResult<TalkToUserResponse>> GetAllByUserTalk(string talkId, string userId)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null)
            {
                return new()
                {
                    Success = false,
                    MessageError = $"User with id {userId} not exists"
                };
            };

            var talk = await _talkRepository.GetTaslkById(talkId);
            if (talk == null)
            {
                return new()
                {
                    Success = false,
                    MessageError = $"Object talk with id:{talk} no exists"
                };
            }

            var tt = await _talkToUserRepository.GetByUserAndTalk(userId,talkId);
            if (tt is null)
            {
                return new()
                {
                    Success = false,
                    MessageError = "Fail, user dont have object userToTalk in this talk"
                };
            }

            var talkResponses = new TalkToUserResponse()
            {
                Id = tt.Id,
                DataCreated = tt.DataCreated,
                IdTalk = tt.IdTalk,
                IdUser = tt.IdUser,
                IsArchived = tt.IsArchived,
                Username = tt.User.Name
            };
            return new()
            {
                Success = true,
                Data = talkResponses
            };


        }

        public async Task<ServiceResult<TalkToUserResponse>> GetById(string id)
        {
   
            var tt = await _talkToUserRepository.GetById(id);
            if (tt is null)
            {
                return new()
                {
                    Success = false,
                    MessageError = "Fail, user dont have object userToTalk in this talk"
                };
            }

            var talkResponses = new TalkToUserResponse()
            {
                Id = tt.Id,
                DataCreated = tt.DataCreated,
                IdTalk = tt.IdTalk,
                IdUser = tt.IdUser,
                IsArchived = tt.IsArchived,
                Username = tt.User.Name
            };
            return new()
            {
                Success = true,
                Data = talkResponses
            };

        }
    }
}
