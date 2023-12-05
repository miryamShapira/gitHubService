using Microsoft.Extensions.Options;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubService
{
    public interface IGitHubService
    {

        public  Task<int> GetUserFollowersAsync();
        public Task<List<Repository>> SearchRepositoryAsync(string? repoName, Language? language, string? userName);

        public  Task<List<Portfolio>> GetPortfolioAsync();
        public Task<int> GetUserIdAsync();
      
    }
}
