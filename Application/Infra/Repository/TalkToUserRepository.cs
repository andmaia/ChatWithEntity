using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Crosscuting.Exceptions;
using Application.Domain.Entity;
using Application.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Application.Infra.Repository
{
    public class TalkToUserRepository : ITalkToUserRepository
    {
        private readonly AppDbContext _context;

        public TalkToUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TalkToUser> Create(TalkToUser talkToUser)
        {
            try
            {
                _context.TalksToUsers.Add(talkToUser);
                await _context.SaveChangesAsync();
                return talkToUser;
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error creating TalkToUser.", ex);
            }
        }

        public async Task<IEnumerable<TalkToUser>> GetAllByTalk(string id)
        {
            try
            {
                var talkToUsers = await _context.TalksToUsers
                    .Include(t=>t.User)
                    .Where(tu => tu.IdTalk == id)
                    .ToListAsync();

                return talkToUsers;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error getting TalkToUsers by Talk ID.", ex);
            }
        }

        public async Task<IEnumerable<TalkToUser>> GetAllByUser(string id)
        {
            try
            {
                var talkToUsers = await _context.TalksToUsers
                    .Include(t=>t.User)
                    .Where(tu => tu.IdUser == id)
                    .ToListAsync();

                return talkToUsers;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error getting TalkToUsers by User ID.", ex);
            }
        }

        public async Task<TalkToUser> GetById(string id)
        {
            try
            {

                var talkToUser = await _context.TalksToUsers
                    .Include(t => t.User)
                    .FirstOrDefaultAsync(t => t.Id == id);
                return talkToUser;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error getting TalkToUser by ID.", ex);
            }
        }

        public async Task<TalkToUser> GetByUserAndTalk(string userId, string talkId)
        {
            try
            {
                var talkToUser = await _context.TalksToUsers
                    .FirstOrDefaultAsync(tu => tu.IdUser == userId && tu.IdTalk == talkId);

                return talkToUser;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error getting TalkToUser by User ID and Talk ID.", ex);
            }
        }

        public async Task<TalkToUser> Update(TalkToUser talkToUser)
        {
            try
            {
                _context.TalksToUsers.Update(talkToUser);
                await _context.SaveChangesAsync();
                return talkToUser;
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error updating TalkToUser.", ex);
            }
        }
    }
}
