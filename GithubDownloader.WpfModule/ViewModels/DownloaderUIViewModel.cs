using Octokit;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GithubDownloader.WpfModule.ViewModels
{
    public class DownloaderUIViewModel : BindableBase
    {
        private string testLink = "https://github.com/Stira-sa/CalibrationSoftware3/releases/download/v0.9.7653.17939/CalibrationSoftware3.v0.9.7653.17939.zip";
        private string testLink2 = "https://github.com/Touseefelahi/WpfFlatStyle/archive/1.6.2.zip";

        private string fullAccess2 = "YourToken";

        public DownloaderUIViewModel()
        {
            // var client = new HttpClient(); WebClient myWebClient = new WebClient();
            // myWebClient.DownloadFile(testLink2, "a.zip");
            Test();
        }

        private async void Test()
        {
            var client = new GitHubClient(new ProductHeaderValue("ReleaseDownloader"));
            var tokenAuth = new Credentials(fullAccess2);
            client.Credentials = tokenAuth;
            //var repo = await client.Repository.Get("Touseefelahi", "RapidHarnessTableFormater");
            var releases = await client.Repository.Release.GetAll("Stira-sa", "CalibrationSoftware3").ConfigureAwait(false);
        }
    }
}