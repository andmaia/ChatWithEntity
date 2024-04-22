using Application.Crosscuting.DTO.TalkToUser;
using Application.Crosscuting.DTO.User;
using Application.Crosscuting.Helpers;
using Application.Domain.Entity;
using Application.Domain.Repository;
using Application.Domain.Service;
using Application.Infra.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Service
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserCredentialsRepository _userCredentialsRepository;

        public UserService(IUserRepository userRepository, IUserCredentialsRepository userCredentialsRepository)
        {
            _userRepository = userRepository;
            _userCredentialsRepository = userCredentialsRepository;
        }

        public async Task<ServiceResult<UserResponse>> CreateUser(UserRequest data)
        {
            var credentialsVerification =await _userCredentialsRepository.FindByIdAsync(data.IdCredentials);
            if (credentialsVerification == null) 
            {
                return new()
                {
                    MessageError = "Credênciais não existem.",
                    Success = false,
                };
            }

            User user = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = data.Name,
                DataCreated = DateTime.Now,
                DateFinished = DateTime.MinValue,
                DateUpdated = DateTime.MinValue,
                IsActive = true,
                IdentityUser = credentialsVerification,
                IdentityUserId = data.IdCredentials
            };

            var result = await _userRepository.CreateUser(user);
            UserResponse userResponse = new()
            {
                Id = result.Id,
                Name = result.Name,
                DataCreated = result.DataCreated,
                IsActive = result.IsActive,
            };

            return new()
            {
                Data = userResponse,
                Success = true
            };
        }

        public async Task<ServiceResult<IEnumerable<UserResponse>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            if (users == null || !users.Any())
            {
                return new()
                {
                    Success = true,
                    Data = Enumerable.Empty<UserResponse>(),
                };
            }

            var usersResponses = users.Select(tt =>
            new UserResponse()
            {
                Id = tt.Id,
                Name = tt.Name,
                DataCreated = tt.DataCreated,
                IsActive=tt.IsActive,
            }
            ).ToList();
            return new()
            {
                Success = true,
                Data = usersResponses
            };
        }

        public async Task<ServiceResult<UserResponse>> GetUserById(string id)
        {
                var result = await _userRepository.GetUserById(id);
                if (result == null)
                {
                    return new()
                    {
                        Success = false,
                        MessageError = "Usuário não existe"
                    };
                }
                UserResponse userResponse = new()
                {
                    Id = result.Id,
                    Name = result.Name,
                    DataCreated = result.DataCreated,
                    IsActive = result.IsActive,
                    PhotoUser = result.PhotoUser,
                };


                return new()
                {
                    Data = userResponse,
                    Success = true
                };

        }

        public async Task<ServiceResult<UserResponse>> GetUserByCredentialsId(string id)
        {
            var result = await _userRepository.GetByUserIdCredentials(id);
            if (result == null)
            {
                return new()
                {
                    Success = false,
                    MessageError = "Usuário não existe"
                };
            }
            UserResponse userResponse = new()
            {
                Id = result.Id,
                Name = result.Name,
                DataCreated = result.DataCreated,
                IsActive = result.IsActive,
                PhotoUser = result.PhotoUser,
            };


            return new()
            {
                Data = userResponse,
                Success = true
            };

        }

        public async Task<ServiceResult<UserResponse>> UpdatePhotoUser(byte[] data, string userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                return new()
                {
                    Success = false,
                    MessageError = "Usuário não existe"
                };
            }

            user.PhotoUser = data;
            var result = await _userRepository.UpdateUser(user);

            if (result!=null)
            {
                UserResponse userResponse = new()
                {
                    Id = result.Id,
                    Name = result.Name,
                    DataCreated = result.DataCreated,
                    IsActive = result.IsActive,
                    PhotoUser = data,
                };


                return new()
                {
                    Data = userResponse,
                    Success = true
                };
            }

            return new()
            {
                Success = false,
                MessageError = "Erro ao adicionar sua foto de perfil"
            };


        }

        public async Task<ServiceResult<UserResponse>> UpdateUser(UserUpdate data)
        {
            var user = await _userRepository.GetUserById(data.Id);
            if (user == null)
            {
                return new()
                {
                    Success = false,
                    MessageError = "Usuário não existe"
                };
            }

            if (data.Name != null)
            {
                user.Name = data.Name;
                var result = await _userRepository.UpdateUser(user);
                return new()
                {
                    Success = result != null,
                    Data = new UserResponse(user.Id, user.Name, user.DataCreated, user.IsActive, user.PhotoUser)
                };
            }

            return new()
            {
                Success = false,
                MessageError="Nem um dado enviado para atualizar"
            };

        }
    }
}
