using ElectionData.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionData.Scraper.Services
{
    public class PollCleanerService : IPollCleanerService
    {
        public async Task<IEnumerable<CleanPoll>> CleanPolls(IEnumerable<DirtyPoll> dirtyPolls)
        {
            return await Task.Run(() =>
            {
                var cleanPolls = new List<CleanPoll>();

                foreach (var dirtyPoll in dirtyPolls)
                {
                    // The date range the format "MM/DD - MM/DD"
                    var dateParts = dirtyPoll.Date.Split(" - ");
                    var cleanPoll = new CleanPoll();

                    cleanPoll.PollLink = dirtyPoll.PollLink;
                    cleanPoll.Pollster = dirtyPoll.Pollster;
                    cleanPoll.StartDate = null;
                    cleanPoll.EndDate = null;
                    cleanPoll.Trump = 0m;
                    cleanPoll.Harris = 0m;
                    cleanPoll.SampleSize = null;
                    cleanPoll.SampleType = null;
                    cleanPoll.MoE = null;
                    cleanPoll.Spread = dirtyPoll.Spread;


                    if (dateParts.Length == 2)
                    {
                        var today = DateTime.Today;

                        // Parse the date into a start date and end date, just put the year as 2024 for now,
                        // unless the month and day is after today, put 2023
                        // Parse start date
                        var startDateParts = dateParts[0].Split('/');
                        var startMonth = int.Parse(startDateParts[0]);
                        var startDay = int.Parse(startDateParts[1]);
                        var startYear = (startMonth > today.Month || (startMonth == today.Month && startDay > today.Day)) ? 2023 : 2024;
                        var startDateString = $"{startMonth}/{startDay}/{startYear}";

                        // Parse end date
                        var endDateParts = dateParts[1].Split('/');
                        var endMonth = int.Parse(endDateParts[0]);
                        var endDay = int.Parse(endDateParts[1]);
                        var endYear = (endMonth > today.Month || (endMonth == today.Month && endDay > today.Day)) ? 2023 : 2024;
                        var endDateString = $"{endMonth}/{endDay}/{endYear}";

                        if (DateOnly.TryParse(startDateString, out DateOnly startDate) && DateOnly.TryParse(endDateString, out DateOnly endDate))
                        {
                            cleanPoll.StartDate = startDate;
                            cleanPoll.EndDate = endDate;
                        }
                        else
                        {
                            Console.WriteLine($"FAILED TO PARSE DATES: {dirtyPoll.Date}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"UNEXPECTED DATE FORMAT! FAILED TO PARSE DATES: {dirtyPoll.Date}");
                    }

                    if (dirtyPoll.Trump != null)
                    {
                        try
                        {
                            cleanPoll.Trump = decimal.Parse(dirtyPoll.Trump);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"FAILED TO PARSE TRUMP for pollster {dirtyPoll.Pollster}: {dirtyPoll.Trump}");
                        }
                    }

                    if (dirtyPoll.Harris != null)
                    {
                        try
                        {
                            cleanPoll.Harris = decimal.Parse(dirtyPoll.Harris);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"FAILED TO PARSE HARRIS for pollster {dirtyPoll.Pollster}: {dirtyPoll.Harris}");
                        }
                    }

                    if (dirtyPoll.Sample != null)
                    {
                        try
                        {
                            // Extract numeric part
                            var numericPart = new string(dirtyPoll.Sample.Where(char.IsDigit).ToArray());
                            cleanPoll.SampleSize = int.TryParse(numericPart, out int sampleSize) ? sampleSize : (int?)null;

                            // Extract non-numeric part
                            var nonNumericPart = new string(dirtyPoll.Sample.Where(c => !char.IsDigit(c)).ToArray());
                            cleanPoll.SampleType = !string.IsNullOrEmpty(nonNumericPart) ? nonNumericPart : null;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"FAILED TO PARSE SAMPLE for pollster {dirtyPoll.Pollster}: {dirtyPoll.Sample}");
                        }
                    }

                    if (dirtyPoll.MoE != null && dirtyPoll.MoE != "—")
                    {
                        try
                        {
                            cleanPoll.MoE = decimal.Parse(dirtyPoll.MoE);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"FAILED TO PARSE MOE for pollster {dirtyPoll.Pollster}: {dirtyPoll.MoE}");
                        }
                    }

                    cleanPolls.Add(cleanPoll);
                }

                return cleanPolls;
            });

        }
    }
}
