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
            var poll = await _context.Polls.FirstOrDefaultAsync(p => p.PollLink == pollLink);
            return poll != null;
        }

        public async Task<IEnumerable<CleanPoll>> GetAllAsync()
        {
            return await _context.Polls.ToListAsync();
        }

        public async Task<decimal> GetAverageForCandidate(string candidateName)
        {
            if (candidateName != "Trump" || candidateName != "Harris")
            {
                throw new ArgumentException("Invalid candidate name");
            }

            if(candidateName == "Trump")
            {
                return await _context.Polls.AverageAsync(p => p.Trump);
            }

            return await _context.Polls.AverageAsync(p => p.Harris);
        }

        public async Task<CleanPoll?> GetLatestPoll()
        {
            return await _context.Polls.OrderByDescending(p => p.EndDate).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CleanPoll>> GetPollsByLikelyVoters()
        {
            return await _context.Polls.Where(p => p.SampleType == "LV").ToListAsync();
        }

        public async Task<IEnumerable<CleanPoll>> GetPollsByRegisteredVoters()
        {
            return await _context.Polls.Where(p => p.SampleType == "RV").ToListAsync();
        }
    }
}
