using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubService
{
    public class Portfolio
    {
        public string language { get; set; }
       public int portfolioId { get; set; }
       public int stars { get; set; }
       public string lastCommit { get; set; }     
       public string link { get; set; }
       
       public int pullRequestCnt { get; set; }

       private static int cnt = 0;
        public Portfolio(string language,int stars,string lastCommit,string link,int pullRequestCnt)
        {
            this.portfolioId = cnt++;
            this.language = language;
            this.stars = stars;
            this.lastCommit = lastCommit;
            this.link = link;
            this.pullRequestCnt = pullRequestCnt;
        }
    }
}
