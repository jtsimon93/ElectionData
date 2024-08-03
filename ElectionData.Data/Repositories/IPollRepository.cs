using ElectionData.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionData.Data.Repositories
{
    public interface IPollRepository
    {
        Task AddAsync(CleanPoll poll);
        Task<bool> CheckIfPollExists(string pollLink);
        Task<IEnumerable<CleanPoll>> GetAllAsync();
    }
}
