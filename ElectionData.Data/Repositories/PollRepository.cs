using ElectionData.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionData.Data.Repositories
{
    public class PollRepository : IPollRepository
    {
        private readonly ElectionDataDbContext _context;

        public PollRepository(ElectionDataDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CleanPoll poll)
        {
            await _context.Polls.AddAsync(poll);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfPollExists(string pollLink)
        {
            var poll = await _context.Polls.FindAsync(pollLink);
            return poll != null;
        }

        public async Task<IEnumerable<CleanPoll>> GetAllAsync()
        {
            return await _context.Polls.ToListAsync();
        }
    }
}
