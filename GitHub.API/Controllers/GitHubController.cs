using GitHubService;
using Microsoft.AspNetCore.Mvc;
using Octokit;

namespace GitHub.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitHubController : ControllerBase
    {

        private readonly IGitHubService _gitService;


        public GitHubController(IGitHubService gitService)
        {

            _gitService = gitService;
        }

        [HttpPost]
        public async Task<List<Repository>> PostAsync([FromBody] SearchParameters request)
        {
            return await _gitService.SearchRepositoryAsync(request?.repoName, request?.language, request?.userName);

        }
        

        [HttpGet]
        public async Task<List<Portfolio>> GetAllAsync()
        {
            return await _gitService.GetPortfolioAsync();

        }
    }
}