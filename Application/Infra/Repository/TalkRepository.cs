using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Crosscuting.Exceptions;
using Application.Domain.Entity;
using Application.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Application.Infra.Repository
{
    public class TalkRepository : ITalkRepository
    {
        private readonly AppDbContext _context;

        public TalkRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Talk> CreateTalk(Talk data)
        {
            try
            {
                _context.Talks.Add(data);
                await _context.SaveChangesAsync();
                return data;
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error creating talk.", ex);
            }
        }

        public async Task<IEnumerable<Talk>> GetTalksByUserId(string id)
        {
            try
            {
                var talks = await _context.Talks
                    .Where(t => t.TalkToUsers.Any(tu => tu.IdUser == id))
                    .ToListAsync();

                return talks;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error getting talks by user ID.", ex);
            }
        }

        public async Task<Talk> GetTaslkById(string id)
        {
            try
            {
                var talk = await _context.Talks.FindAsync(id);
                return talk;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error getting talk by ID.", ex);
            }
        }
    }
}
