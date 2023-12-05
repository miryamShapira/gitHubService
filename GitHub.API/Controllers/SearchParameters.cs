using Octokit;

namespace GitHub.API.Controllers
{
    public class SearchParameters
    {
       public string? repoName { get; set; }
        public Language? language { get; set; } 
       public  string? userName { get; set; }
        
    }
}