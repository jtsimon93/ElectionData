using ElectionData.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ElectionData.API.Functions.HttpTriggers
{
    public class GetAggregatedPollDataByPollsterFunction
    {
        private readonly ILogger<GetAggregatedPollDataByPollsterFunction> _logger;
        private readonly IPollService _pollService;

        public GetAggregatedPollDataByPollsterFunction(ILogger<GetAggregatedPollDataByPollsterFunction> logger, IPollService pollService)
        {
            _logger = logger;
            _pollService = pollService;
        }

        [Function("GetAggregatedPollDataByPollster")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            _logger.LogInformation("GetAggregatedPollDataByPollster called");
            var polls = await _pollService.GetAggregatedPollDataByPollsterAsync();
            return new OkObjectResult(polls);
        }
    }
}
