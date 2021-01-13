using Octokit;
using System.Threading.Tasks;

namespace GithubDownloader.Core.Services
{
    public class Client
    {
        private readonly GitHubClient client;
        private bool isAuthenticated;

        public Client()
        {
            client = new GitHubClient(new ProductHeaderValue("ReleaseDownloader"));
        }

        /// <summary>
        /// Initialize the client and get the user with provided token. If use is found it returns true
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Token Status</returns>
        public async Task<bool> AuthenticateAndConfirm(string token)
        {
            Authenticate(token);
            User user = await client.User.Current().ConfigureAwait(false);
            return user is not null;
        }

        /// <summary>
        /// Initializes the Github client
        /// </summary>
        /// <param name="token"></param>
        public void Authenticate(string token)
        {
            client.Credentials = new Credentials(token);
            isAuthenticated = true;
        }

        /// <summary>
        /// Get repository
        /// </summary>
        /// <param name="configuration">
        /// Configuration containing valid token, repository and owner name
        /// </param>
        /// <returns>Repository</returns>
        public async Task<Repository> GetRepository(IConfiguration configuration)
        {
            if (configuration.Token is not null && isAuthenticated is not true)
            {
                Authenticate(configuration.Token);
            }
            return await GetRepository(configuration.OwnerName, configuration.RepositoryName).ConfigureAwait(false);
        }

        /// <summary>
        /// Get repository
        /// </summary>
        /// <param name="ownerName">Owner Name</param>
        /// <param name="repositoryName">Repository name</param>
        /// <param name="token">Token</param>
        /// <returns>Repository</returns>
        public async Task<Repository> GetRepository(string ownerName, string repositoryName, string token = null)
        {
            var configuration = new Configuration()
            {
                Token = token,
                OwnerName = ownerName,
                RepositoryName = repositoryName
            };
            return await GetRepository(configuration).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the repository (Client must be authenticated for private repository)
        /// </summary>
        /// <param name="ownerName"></param>
        /// <param name="repositoryName"></param>
        /// <returns>Repository</returns>
        public async Task<Repository> GetRepository(string ownerName, string repositoryName)
        {
            return await client.Repository.Get(ownerName, repositoryName).ConfigureAwait(false);
        }

        //public async Task<IReleasesClient> GetReleaseAsync(IConfiguration configuration)
        //{
        //   // return await client.Repository.Release.GetAll(configuration.OwnerName, configuration.RepositoryName).ConfigureAwait(false);

        //}
    }
}