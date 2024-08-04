using ElectionData.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ElectionData.API.Functions.HttpTriggers
{
    public class GetPollsByRegisteredVotersFunction
    {
        private readonly ILogger<GetPollsByRegisteredVotersFunction> _logger;
        private readonly IPollService _pollService;

        public GetPollsByRegisteredVotersFunction(ILogger<GetPollsByRegisteredVotersFunction> logger, IPollService pollService)
        {
            _logger = logger;
            _pollService = pollService;
        }

        [Function("GetPollsByRegisteredVoters")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            _logger.LogInformation("GetPollsByRegisteredVoters called");
            var polls = await _pollService.GetPollsByRegisteredVotersAsync();
            return new OkObjectResult(polls);
        }
    }
}
