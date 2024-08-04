using ElectionData.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ElectionData.API.Functions.HttpTriggers
{
    public class GetAggregatedPollDataByEndDateFunction
    {
        private readonly ILogger<GetAggregatedPollDataByEndDateFunction> _logger;
        private readonly IPollService _pollService;

        public GetAggregatedPollDataByEndDateFunction(ILogger<GetAggregatedPollDataByEndDateFunction> logger, IPollService pollService)
        {
            _logger = logger;
            _pollService = pollService;
        }

        [Function("GetAggregatedPollDataByEndDate")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("GetAggregatedPollDataByEndDate called");
            var pollData = await _pollService.GetAggregatedPollDataByEndDateAsync();
            return new OkObjectResult(pollData);
        }
    }
}
