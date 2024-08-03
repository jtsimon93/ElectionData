using ElectionData.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionData.Scraper.Services
{
    public class PollProcessorService : IPollProcessorService
    {
        private readonly Func<string, IPollCollectorService> _pollCollectorServiceFactory;
        private readonly IPollService _pollService;
        private readonly IPollCleanerService _pollCleanerService;

        public PollProcessorService(IPollService pollService, IPollCleanerService pollCleanerService, Func<string, IPollCollectorService> pollCollectorServiceFactory)
        {
            _pollService = pollService;
            _pollCollectorServiceFactory = pollCollectorServiceFactory;
            _pollCleanerService = pollCleanerService;
        }

        public bool ProcessAllPolls()
        {
            try
            {
                // Get the polls
                var pollCollectorService = _pollCollectorServiceFactory("RealClear");
                var dirtyPolls = pollCollectorService.GetPollsAsync().GetAwaiter().GetResult();

                // Clean the data
                var cleanPolls = _pollCleanerService.CleanPolls(dirtyPolls).GetAwaiter().GetResult();

                // Persist the data
                _pollService.ProcessIncomingPolls(cleanPolls).GetAwaiter().GetResult();

                return true;
            } catch(Exception ex)
            {
                Console.WriteLine($"Error processing polls: {ex.Message}");
                return false;
            }
        }
    }
}
