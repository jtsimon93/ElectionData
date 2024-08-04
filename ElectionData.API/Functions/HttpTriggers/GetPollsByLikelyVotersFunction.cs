using ElectionData.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ElectionData.API.Functions.HttpTriggers
{
    public class GetPollsByLikelyVotersFunction
    {
        private readonly ILogger<GetPollsByLikelyVotersFunction> _logger;
        private readonly IPollService _pollService;

        public GetPollsByLikelyVotersFunction(ILogger<GetPollsByLikelyVotersFunction> logger, IPollService pollService)
        {
            _logger = logger;
            _pollService = pollService;
        }

        [Function("GetPollsByLikelyVoters")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            _logger.LogInformation("GetPollsByLikelyVotersFunction called");
            var polls = await _pollService.GetPollsByLikelyVotersAsync();
            return new OkObjectResult(polls);
        }
    }
}
