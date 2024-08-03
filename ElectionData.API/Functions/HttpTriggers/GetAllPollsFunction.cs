using ElectionData.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ElectionData.API.Functions.HttpTriggers
{
    public class GetAllPollsFunction
    {
        private readonly ILogger<GetAllPollsFunction> _logger;
        private readonly IPollService _pollService;

        public GetAllPollsFunction(ILogger<GetAllPollsFunction> logger, IPollService pollService)
        {
            _logger = logger;
            _pollService = pollService;
        }

        [Function("GetAllPollsFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            _logger.LogInformation("GetAllPollsFunction called");

            var polls = await _pollService.GetAllPollsAsync();

            return new OkObjectResult(polls);
        }
    }
}
