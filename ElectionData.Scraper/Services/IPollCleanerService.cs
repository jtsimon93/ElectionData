using ElectionData.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionData.Scraper.Services
{
    public interface IPollCleanerService
    {
        Task<IEnumerable<CleanPoll>> CleanPolls(IEnumerable<DirtyPoll> dirtyPolls);
    }
}
