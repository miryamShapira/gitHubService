using Microsoft.Extensions.Options;
using Octokit;
using System.Xml.Linq;

namespace GitHubService
{
    public class GitHubService : IGitHubService
    {
        private readonly GitHubClient _client;
        private readonly GitHubIntegretionOptions _options;
        public GitHubService(IOptions<GitHubIntegretionOptions> options)
        {
            _client = new GitHubClient(new ProductHeaderValue("my-gitHub-app"));

            _options = options.Value;
            _client.Credentials = new Credentials(_options.Tocken);
        }
        public async Task<int> GetUserFollowersAsync()
        {
            var user = await _client.User.Get(_options.UserName);
            var u = user.PublicRepos;
            Console.WriteLine(u.GetType());

            return user.Followers;
        }
        public async Task<List<Repository>> SearchRepositoryAsync(string? repoName, Language? language, string? userName)
        {
           
            SearchRepositoriesRequest request= repoName == null?new SearchRepositoriesRequest():new SearchRepositoriesRequest(repoName);
          
            if (language != null)
                request.Language=language;
            if (userName != null)
                request.User = userName;

            var result = await _client.Search.SearchRepo(request);
            return result.Items.ToList();
        }
        public async Task<List<Portfolio>> GetPortfolioAsync()
        {
            var options = new ApiOptions();
            var repositories = await _client.Repository.GetAllForCurrent(options);
            List<Portfolio> result = new List<Portfolio>();
            foreach (var re in repositories)
            {

                string language = re.Language;
                Console.WriteLine("the repo hase {0} stars", re.StargazersCount);
                try
                {
                    result.Add(new Portfolio(re.Language, re.StargazersCount, _client.Repository.Commit.GetAll(re.Id).Result[0].Commit.Message, re.Url, _client.PullRequest.GetAllForRepository(re.Id).Result.Count));
                    Console.WriteLine("the repo has {0} commits", _client.Repository.Commit.GetAll(re.Id).Result.Count);
                    var c = _client.Repository.Commit.GetAll(re.Id).Result;
                    var first = c[0];
                    Console.WriteLine("the repo  commits  " + _client.Repository.Commit.GetAll(re.Id).Result[0].Label);
                }
                catch
                {
                    Console.WriteLine("error!");
                }

                //var releases =await _client.Repository.Release.GetAll(re.Id);
                //if(null != releases[0])
                //Console.WriteLine("the repo's last commit is {0}", releases[0].Name);
                Console.WriteLine("the link is {0}", re.Url);
                Console.WriteLine(_client.PullRequest.GetAllForRepository(re.Id).Result.Count);


            }
            return result;
        }

        public async Task<int> GetUserIdAsync()
        {
            var user = await _client.User.Get(_options.UserName);
            return user.Id;
        }


    }
}