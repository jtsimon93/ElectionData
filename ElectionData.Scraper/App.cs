using ElectionData.Scraper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionData.Scraper
{
    public class App
    {
        private readonly IPollProcessorService _pollProcessorService;

        public App(IPollProcessorService pollProcessorService)
        {
            _pollProcessorService = pollProcessorService;
        }

        public void Run()
        {
            Console.WriteLine("Scraping app started.");
            var success = _pollProcessorService.ProcessAllPolls();

            if (success)
            {
                Console.WriteLine("Scraping app completed successfully.");
            }
            else
            {
                Console.WriteLine("Scraping app failed.");
            }
        }
    }

}
