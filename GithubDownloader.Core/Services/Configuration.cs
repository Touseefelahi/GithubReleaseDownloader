namespace GithubDownloader.Core
{
    public class Configuration : IConfiguration
    {
        public string Token { get; set; }
        public string RepositoryName { get; set; }
        public string OwnerName { get; set; }
    }
}