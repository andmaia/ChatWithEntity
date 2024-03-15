using Microsoft.AspNetCore.Identity;

namespace Application.Domain.Repository
{
    public interface IUserCredentialsRepository
    {
        Task<IdentityResult> AddUserAsync(IdentityUser user, string password);
        Task<SignInResult> LoginAsync(string email, string password);
        Task<IdentityUser> FindByEmailAsync(string email);
        Task<IdentityUser> FindByIdAsync(string userId);
    }
}
