using ElectionData.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ElectionData.API.Functions.HttpTriggers
{
    public class GetCandidateAveragesFunction
    {
        private readonly ILogger<GetCandidateAveragesFunction> _logger;
        private readonly IPollService _pollService;

        public GetCandidateAveragesFunction(ILogger<GetCandidateAveragesFunction> logger, IPollService pollService)
        {
            _logger = logger;
            _pollService = pollService;
        }

        [Function("GetCandidateAverages")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            _logger.LogInformation("GetCandidateAverages called");
            var averages = await _pollService.GetCandidateAveragesAsync();
            return new OkObjectResult(averages);
        }
    }
}
