﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Crosscuting.Exceptions;
using Application.Domain.Entity;
using Application.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Application.Infra.Repository
{
    public class MessageTalkToUserRepository : IMessageTalkToUserRepository
    {
        private readonly AppDbContext _context;

        public MessageTalkToUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Create(MessageTallkToUser data)
        {
            try
            {
                _context.MessageTallkToUsers.Add(data);
                await _context.SaveChangesAsync();
                return data.Id; // Assuming Id is generated by the database
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exception appropriately
                throw new RepositoryException("Error creating MessageTalkToUser.", ex);
            }
        }

        public async Task<IEnumerable<MessageTallkToUser>> GetByAllByMessageId(string id)
        {
            return await _context.MessageTallkToUsers
                .Where(mtu => mtu.MessageId == id)
                .ToListAsync();
        }

        public async Task<IEnumerable<MessageTallkToUser>> GetByAllByTalkToUserId(string id)
        {
            return await _context.MessageTallkToUsers
                .Where(mtu => mtu.TalkToUserId == id)
                .ToListAsync();
        }

        public async Task<MessageTallkToUser> GetById(string id)
        {
            return await _context.MessageTallkToUsers.FindAsync(id);
        }
    }
}
