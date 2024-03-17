using System;
using System.Threading.Tasks;
using Application.Crosscuting.Exceptions;
using Application.Domain.Entity;
using Application.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Application.Infra.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User data)
        {
            try
            {
                _context.User.Add(data);
                await _context.SaveChangesAsync();
                return data;
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error creating user.", ex);
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                var talkToUsers = await _context.User
      .Include(u => u.IdentityUser)
      .AsNoTracking()
      .Select(u => new User
      {
          Id = u.Id,
          Name = u.IdentityUser.UserName,
          DataCreated = u.DataCreated,
          IsActive = u.IsActive,
      })
      .ToListAsync();
                return talkToUsers;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error getting TalkToUsers by User ID.", ex);
            }
        }

        public async Task<User> GetUserById(string id)
        {
            try
            {
                var result = await _context.User.FindAsync(id);
                return result;
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException)
            {
                throw new RepositoryException("Error getting user by ID.", ex);
            }
        }

        public async Task<User> UpdateUser(User data)
        {
            try
            {
                _context.User.Update(data);
                await _context.SaveChangesAsync();
                return data;
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error updating user.", ex);
            }
        }
    }
}
