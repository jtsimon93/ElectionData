using OpenQA.Selenium.Chrome;
using HtmlAgilityPack;
using ElectionData.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionData.Scraper.Services
{
    public class RealClearPollingCollectorService : IPollCollectorService
    {
        public async Task<IEnumerable<DirtyPoll>> GetPollsAsync()
        {
            var url = "https://www.realclearpolling.com/polls/president/general/2024/trump-vs-harris";
            var options = new ChromeOptions();
            options.AddArgument("headless"); // Run Chrome in headless mode
            options.AddArgument("disable-gpu"); // Applicable to Windows OS only
            options.AddArgument("no-sandbox"); // Bypass OS security model

            using (var driver = new ChromeDriver(options))
            {
                driver.Navigate().GoToUrl(url);

                // Wait for the page to load completely
                await Task.Delay(5000); // Adjust delay as needed for the page to load

                // Get the page source after it has fully loaded
                var pageSource = driver.PageSource;

                var doc = new HtmlDocument();
                doc.LoadHtml(pageSource);

                var pollNodes = doc.DocumentNode.SelectNodes("(//table[contains(@class, 'w-full')])[2]//tr");
                if (pollNodes == null)
                {
                    Console.WriteLine("No poll data found on the webpage.");
                    return Enumerable.Empty<DirtyPoll>();
                }

                var polls = new List<DirtyPoll>();

                foreach (var row in pollNodes.Skip(1)) // Skip header row
                {
                    var columns = row.SelectNodes("td")?.Select(td => td.InnerText.Trim()).ToArray();
                    var pollsterLinkNode = row.SelectSingleNode("td/a");

                    if (columns == null)
                    {
                        Console.WriteLine("No columns found in the row.");
                        continue;
                    }

                    if (columns[0] == "RCP Average")
                    {
                        Console.WriteLine("Skipping an average row");
                        continue;
                    }

                    if (columns.Length < 7)
                    {
                        Console.WriteLine("The row does not have the expected number of columns.");
                        continue;
                    }

                    string? pollLink = null;
                    pollLink = pollsterLinkNode?.GetAttributeValue("href", null);

                    // If the poll link doesn't exist, create some placeholder text that is likely to be unique
                    // For now we will use the pollster and date
                    if (pollLink == null)
                    {
                        pollLink = $"NOLINK: {columns[0]} {columns[1]}";
                    }

                    var poll = new DirtyPoll
                    {
                        Pollster = columns[0],
                        Date = columns[1],
                        Sample = columns[2],
                        MoE = columns[3],
                        Trump = columns[4],
                        Harris = columns[5],
                        Spread = columns[6],
                        PollLink = pollLink
                    };

                    polls.Add(poll);
                }

                return polls;
            }
        }
    }
}
