using Application.Crosscuting.DTO.Message;
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
    public class MessageService : IMessageService
    {

        private readonly ITalkToUserRepository _talkToUserRepository;
        private readonly ITalkRepository _talkRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMessageTalkToUserRepository _messageTalkToUserRepository;

        public MessageService(ITalkToUserRepository talkToUserRepository, ITalkRepository talkRepository, IUserRepository userRepository, IMessageRepository messageRepository, IMessageTalkToUserRepository messageTalkToUserRepository)
        {
            _talkToUserRepository = talkToUserRepository;
            _talkRepository = talkRepository;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _messageTalkToUserRepository = messageTalkToUserRepository;
        }

        public async Task<ServiceResult<MessageResponse>> CreateMessage(MessageRequest data)
        {
            var talk = await _talkRepository.GetTaslkById(data.TalkId);
            if (talk == null)
            {
                return new()
                {
                    Success = false,
                    MessageError = $"Object talk with id:{data.TalkId} no exists"
                };
            }

            var user = await _userRepository.GetUserById(data.UserId);
            if (user == null)
            {
                return new()
                {
                    Success = false,
                    MessageError = $"User with id: {data.UserId} does not exist",
                };
            }

            Message message = new()
            {
                Id = Guid.NewGuid().ToString(),
                DateCreate = DateTime.Now,
                DateFinished = DateTime.MinValue,
                DateUpdate = DateTime.MinValue,
                IsActive = true,
                Text = data.Text,
                UserId = data.UserId,
                TalkId = data.TalkId,
                Talk = talk,
                User = user
            };

            var result = await _messageRepository.Create(message);

            MessageResponse messageResponse = new MessageResponse()
            {
                Id = message.Id,
                DateCreate = message.DateCreate,
                TalkId = message.TalkId,
                Text = message.Text,
                UserId = message.UserId,
            };

            return new()
            {
                Data = messageResponse,
                Success = result
            };

        }

        public async Task<ServiceResult<IEnumerable<MessageResponse>>> GetAllbyTalk(string talkId)
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

            var messages = await _messageRepository.GetAllbyTalk(talkId);
            if (messages == null || !messages.Any())
            {
                return new()
                {
                    Success = true,
                    Data = Enumerable.Empty<MessageResponse>(),
                };
            }

            var messagesToRespoonse = messages.Select(m=>new MessageResponse(m.Id,m.Text,m.DateCreate,m.UserId,m.TalkId)).ToList();
            return new()
            {
                Success = true,
                Data = messagesToRespoonse
            };
        }

        public async Task<ServiceResult<IEnumerable<MessageResponse>>> GetAllbyTalkAndUser(string talkId, string userId)
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

            var messages = await _messageRepository.GetAllbyTalkAndUser(talkId,userId);
            if (messages == null || !messages.Any())
            {
                return new()
                {
                    Success = true,
                    Data = Enumerable.Empty<MessageResponse>(),
                };
            }

            var messagesToRespoonse = messages.Select(m => new MessageResponse(m.Id, m.Text, m.DateCreate, m.UserId, m.TalkId)).ToList();
            return new()
            {
                Success = true,
                Data = messagesToRespoonse
            };
        }

        public async Task<ServiceResult<IEnumerable<MessageResponse>>> GetAllbyTalkToUserId(string userId)
        {
            var talkToUser = await _talkToUserRepository.GetById(userId);
            if (talkToUser == null)
            {
                return new()
                {
                    Success = false,
                    MessageError = $"Object talkToUser with id:{userId} no exists"
                };
            }

            var messages = await _messageRepository.GetAllbyTalkToUserId(userId);
            if (messages == null || !messages.Any())
            {
                return new()
                {
                    Success = true,
                    Data = Enumerable.Empty<MessageResponse>(),
                };
            }

            var messagesToRespoonse = messages.Select(m => new MessageResponse(m.Id, m.Text, m.DateCreate, m.UserId, m.TalkId)).ToList();
            return new()
            {
                Success = true,
                Data = messagesToRespoonse
            };

        }

        public async Task<ServiceResult<MessageResponse>> GetById(string id)
        {
            var message = await _messageRepository.GetById(id);
            if (message == null)
            {
                return new()
                {
                    Success = false,
                    MessageError = $"Object talk with id:{message.Id} no exists"
                };
            }

            var messageResponse = new MessageResponse()
            {
                Id = message.Id,
                DateCreate = message.DateCreate,
                TalkId = message.TalkId,
                UserId = message.UserId,
                Text = message.Text
            };


            return new()
            {
                Success = true,
                Data = messageResponse
            };

        }
    }
}
