using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionData.Scraper.Services
{
    public interface IPollProcessorService
    {
        bool ProcessAllPolls();
    }
}
