using GitHubService;
using Microsoft.Extensions.Caching.Memory;
using Octokit;

namespace GitHub.API.Cached_Services
{
    public class CachedGitHubService : IGitHubService
    {
        private readonly IGitHubService _gitHubService;
        private readonly IMemoryCache _memoryCache;
        private  int userId;

        public  CachedGitHubService(IGitHubService gitHubService,IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _gitHubService = gitHubService;
            userId = GetUserIdAsync().Result;
           
        }

        public async Task<List<Portfolio>> GetPortfolioAsync()
        {
            if (_memoryCache.TryGetValue(userId, out List<Portfolio> re))
                return re;
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(30)).
                 SetSlidingExpiration(TimeSpan.FromSeconds(10));

            re = await _gitHubService.GetPortfolioAsync();
            _memoryCache.Set(userId,re,cacheOptions);
            return re;
        }

        public async Task<int> GetUserFollowersAsync()
        {
           return await (_gitHubService.GetUserFollowersAsync());
        }

        public async Task<int> GetUserIdAsync()
        {
            return await _gitHubService.GetUserIdAsync();
        }

        public async Task<List<Repository>> SearchRepositoryAsync(string? repoName, Language? language, string? userName)
        {
            return await _gitHubService.SearchRepositoryAsync(repoName, language, userName);
        }
    }
}
