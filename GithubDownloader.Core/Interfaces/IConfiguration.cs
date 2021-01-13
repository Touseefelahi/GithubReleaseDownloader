namespace GithubDownloader.Core
{
    public interface IConfiguration
    {
        string OwnerName { get; set; }
        string RepositoryName { get; set; }
        string Token { get; set; }
    }
}