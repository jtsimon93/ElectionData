using AutoMapper;
using ElectionData.Data.Entities;
using ElectionData.Data.Repositories;
using ElectionData.Data.Dtos;
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
        private readonly IMapper _mapper;

        public PollService(IPollRepository pollRepository, IMapper mapper)
        {
            _pollRepository = pollRepository;
            _mapper = mapper;
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

        public async Task<IEnumerable<PollDto>> GetAllPollsAsync()
        {
            var polls = await _pollRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PollDto>>(polls);
        }

        public async Task<CandidateAveragesDto> GetCandidateAveragesAsync()
        {
            var trumpAverage = await _pollRepository.GetAverageForCandidate("Trump");
            var harrisAverage = await _pollRepository.GetAverageForCandidate("Harris");

            return new CandidateAveragesDto
            {
                TrumpAverage = trumpAverage,
                HarrisAverage = harrisAverage
            };
        }

        public async Task<PollDto?> GetLatestPollAsync()
        {
            var poll = await _pollRepository.GetLatestPoll();
            return _mapper.Map<PollDto>(poll);
        }

        public async Task<IEnumerable<PollDto>> GetPollsByLikelyVotersAsync()
        {
            var polls = await _pollRepository.GetPollsByLikelyVoters();
            return _mapper.Map<IEnumerable<PollDto>>(polls);
        }

        public async Task<IEnumerable<PollDto>> GetPollsByRegisteredVotersAsync()
        {
            var polls = await _pollRepository.GetPollsByRegisteredVoters();
            return _mapper.Map<IEnumerable<PollDto>>(polls);
        }

        public async Task<IEnumerable<AggregatedPollDataByEndDateDto>> GetAggregatedPollDataByEndDateAsync()
        {
            var polls = await _pollRepository.GetAllAsync();

            var aggregatedPollData = polls
                .Where(p => p.EndDate.HasValue)
                .GroupBy(p => p.EndDate.Value)
                .Select(g => new AggregatedPollDataByEndDateDto
                {
                    EndDate = g.Key,
                    TrumpAverage = g.Average(p => p.Trump),
                    HarrisAverage = g.Average(p => p.Harris)
                })
                .OrderBy(p => p.EndDate)
                .ToList();

            return aggregatedPollData;
        }
    }
}
