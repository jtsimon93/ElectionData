using ElectionData.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ElectionData.API.Functions.HttpTriggers
{
    public class GetLatestPollFunction
    {
        private readonly ILogger<GetLatestPollFunction> _logger;
        private readonly IPollService _pollService;

        public GetLatestPollFunction(ILogger<GetLatestPollFunction> logger, IPollService pollService)
        {
            _logger = logger;
            _pollService = pollService;
        }

        [Function("GetLatestPollFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            _logger.LogInformation("GetLatestPollFunction called");
            var latestPoll = await _pollService.GetLatestPollAsync();

            return new OkObjectResult(latestPoll);
        }
    }
}
