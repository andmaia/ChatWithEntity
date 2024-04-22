using Application.Crosscuting.DTO.Credentials;
using Application.Crosscuting.Helpers;
using Microsoft.AspNetCore.Identity;

namespace Application.Domain.Service
{
    public interface IUserCredentialsService
    {
        Task<ServiceResult<IdentityResult>> CreateUserAsync(UserCredentialsRequest data);

        Task<ServiceResult<UserLoginResponse>> LoginUserAsync(UserCredentialsLogin data);



    }
}
