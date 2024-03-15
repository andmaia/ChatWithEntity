using Application.Crosscuting.DTO.Credentials;
using Application.Crosscuting.Helpers;
using Application.Domain.Repository;
using Application.Domain.Service;
using Microsoft.AspNetCore.Identity;

namespace Application.Service
{
    public class UserCredentialsService : IUserCredentialsService
    {
        private readonly IUserCredentialsRepository _userCredentialsRepository;
        private readonly TokenService _tokenService;

        public UserCredentialsService(IUserCredentialsRepository userCredentialsRepository, TokenService tokenService)
        {
            _userCredentialsRepository = userCredentialsRepository;
            _tokenService = tokenService;
        }
        public async Task<ServiceResult<IdentityResult>> CreateUserAsync(UserCredentialsRequest data)
        {
            if (!PasswordMatchesConfirmation(data.Password, data.PasswordConfirmation))
            {
                return new()
                {
                    Success =false,
                    Data = IdentityResult.Failed(new IdentityError { Description = "As senhas não coincidem." })
                 };
            }

            var user = new IdentityUser
            {
                UserName = data.Username,
                Email = data.Email
            };

            var result = await _userCredentialsRepository.AddUserAsync(user, data.Password);

            return new()
            {
                Success = result.Succeeded,
                Data = result,
                MessageError = result.Succeeded?string.Empty:"Erro ao criar usuário"
            };
        }

        public async Task<ServiceResult<string>> LoginUserAsync(UserCredentialsLogin data)
        {
           
            var user = await _userCredentialsRepository.FindByEmailAsync(data.Email);

            if (user == null)
            {
                return new()
                {
                    Success = false,
                    MessageError = "Email não pertence a um usuário."
                };
            }

            var loginResult = await _userCredentialsRepository.LoginAsync(user.UserName, data.Password);

            if (loginResult.Succeeded)
            {
                var token = _tokenService.GenerateToken(user);
               return new()
                {
                    Success = true,
                    Data = token
                };
            }
            else
            {
                return new()
                {
                    Success = false,
                    MessageError = "Erro ao logar"
                };
            }
        }

        private bool PasswordMatchesConfirmation(string password, string passwordConfirmation)
        {
            return password == passwordConfirmation;
        }
    }
}
