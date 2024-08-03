using ElectionData.Data.Entities;
using ElectionData.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionData.Data.Services
{
    public class PollService : IPollService
    {
        private readonly IPollRepository _pollRepository;

        public PollService(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public async Task AddPollAsync(CleanPoll poll)
        {
            await _pollRepository.AddAsync(poll);
        }

        public async Task<bool> CheckIfPollExists(string pollLink)
        {
            return await _pollRepository.CheckIfPollExists(pollLink);
        }

        public async Task ProcessIncomingPolls(IEnumerable<CleanPoll> polls)
        {
            foreach (var poll in polls)
            {
                if (!await CheckIfPollExists(poll.PollLink))
                {
                    await AddPollAsync(poll);
                }
            }
        }
    }
}
