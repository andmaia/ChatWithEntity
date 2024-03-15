using System;
using System.Threading.Tasks;
using Application.Crosscuting.Exceptions;
using Application.Domain.Repository;
using Microsoft.AspNetCore.Identity;

namespace Application.Infra.Repository
{
    public class UserCredentialsRepository : IUserCredentialsRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserCredentialsRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddUserAsync(IdentityUser user, string password)
        {
            try
            {
                return await _userManager.CreateAsync(user, password);
            }
            catch (Exception ex)
            {
                throw new UserCreationException("Failed to create user.", ex);
            }
        }

        public async Task<SignInResult> LoginAsync(string username, string password)
        {
            try
            {
                return await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
            }
            catch (Exception ex)
            {
                throw new UserCreationException("Failed to login.", ex);
            }
        }

        public async Task<IdentityUser> FindByEmailAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                return user;
            }
            catch (Exception ex)
            {
                throw new UserCreationException("Error finding user by email.", ex);
            }
        }

        public async Task<IdentityUser> FindByIdAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                return user;
            }
            catch (Exception ex)
            {
                throw new UserCreationException("Error finding user by ID.", ex);
            }
        }
    }
}
