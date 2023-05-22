using System.Reflection;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;

namespace YouTubeMigrationClient
{
    public class YouTubeServiceFactory
    {
        public static async Task<UserCredential> CreateCredential(string credentialFileName)
        {
            UserCredential credential;
            using (var stream = new FileStream(credentialFileName, FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    (await GoogleClientSecrets.FromStreamAsync(stream)).Secrets,
                    new[] { YouTubeService.Scope.Youtube },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(Assembly.GetExecutingAssembly().GetType().ToString())
                );
            }
            return credential;
        }
    }
}
