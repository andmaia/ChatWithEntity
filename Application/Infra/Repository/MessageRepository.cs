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
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _context;

        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Message data)
        {
            try
            {
                _context.Messages.Add(data);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error creating Message.", ex);
            }
        }

        public async Task<IEnumerable<Message>> GetAllbyTalk(string talkId)
        {
            try
            {
                var messages = await _context.Messages
                    .Where(m => m.TalkId == talkId)
                    .ToListAsync();

                return messages;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error getting Messages by Talk ID.", ex);
            }
        }

        public async Task<IEnumerable<Message>> GetAllbyTalkAndUser(string talkId, string userId)
        {
            try
            {
                var messages = await _context.Messages
                    .Where(m => m.TalkId == talkId && m.UserId == userId)
                    .ToListAsync();

                return messages;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error getting Messages by Talk ID and User ID.", ex);
            }
        }

        public async Task<IEnumerable<Message>> GetAllbyTalkToUserId(string userId)
        {
            try
            {
                var messages = await _context.Messages
                    .Where(m => m.MessageTallkToUsers.Any(mu => mu.TalkToUserId == userId))
                    .ToListAsync();

                return messages;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error getting Messages by User ID.", ex);
            }
        }

        public async Task<Message> GetById(string id)
        {
            try
            {
                var message = await _context.Messages.FindAsync(id);
                return message;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error getting Message by ID.", ex);
            }
        }
    }
}
