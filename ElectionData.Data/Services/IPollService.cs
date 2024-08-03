using ElectionData.Data.Dtos;
using ElectionData.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionData.Data.Services
{
    public interface IPollService
    {
        Task AddPollAsync(CleanPoll poll);
        Task<bool> CheckIfPollExists(string pollLink);
        Task ProcessIncomingPolls(IEnumerable<CleanPoll> polls);
        Task<IEnumerable<PollDto>> GetAllPollsAsync();
        Task<CandidateAveragesDto> GetCandidateAveragesAsync();
        Task<PollDto?> GetLatestPollAsync();
    }
}
