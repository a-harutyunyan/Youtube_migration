using System;
using System.Reflection;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using YouTubeMigrationClient;

// Get the subscriptions from the Source Account
//var sourceYouTubeCredential = await YouTubeServiceFactory.CreateCredential("client_secrets_source.json");
//var sourceYouTubeService = new YouTubeService(new BaseClientService.Initializer()
//{
//    HttpClientInitializer = sourceYouTubeCredential,
//    ApplicationName = Assembly.GetExecutingAssembly().GetType().ToString()
//});

//string nextPageToken = null;
//var sourceSubscriptionList = new List<Subscription>();
//do
//{
//    var sourceSubscriptionListRequest = sourceYouTubeService.Subscriptions.List("id,snippet");
//    sourceSubscriptionListRequest.Mine = true;
//    sourceSubscriptionListRequest.MaxResults = 50;
//    sourceSubscriptionListRequest.Order = SubscriptionsResource.ListRequest.OrderEnum.Alphabetical;
//    sourceSubscriptionListRequest.PageToken = nextPageToken;
//    var sourceSubscriptions = await sourceSubscriptionListRequest.ExecuteAsync();
//    nextPageToken = sourceSubscriptions.NextPageToken;
//    sourceSubscriptionList.AddRange(sourceSubscriptions.Items);
//} while (nextPageToken != null);

//Console.WriteLine($"Retrieved {sourceSubscriptionList.Count} subscriptions from the source account");

//await sourceYouTubeCredential.RevokeTokenAsync(new CancellationToken());
// Import subscriptions into the Destination Account

var targetYouTubeCredential = await YouTubeServiceFactory.CreateCredential("client_secrets_destination.json");

//var targetYouTubeCredential = await YouTubeServiceFactory.CreateCredential("client_secrets_source.json");

var targetYouTubeService = new YouTubeService(new BaseClientService.Initializer()
{
    HttpClientInitializer = targetYouTubeCredential,
    ApplicationName = Assembly.GetExecutingAssembly().GetType().ToString()
});

//try
//{
//    await targetYouTubeService.Videos.Delete("pO_VVE55saU").ExecuteAsync();

//}
//catch (Exception EX)
//{

//    Console.WriteLine(EX.Message);
//}


string[] canelList = new string[]
{
   "chanelId"
};

int i = 0;

foreach (var subscription in canelList)
{
    var targetSubscription = new Subscription
    {
        Snippet = new SubscriptionSnippet
        {
            ResourceId = new ResourceId
            {
                Kind = "youtube#subscription",
                ChannelId = subscription
            }
        }
    };

    try
    {
        await targetYouTubeService.Subscriptions.Insert(targetSubscription, "id,snippet").ExecuteAsync();

        Console.WriteLine($"number {i} ChannelId: {subscription}\t\t  Status => Done");
        i++;
    }
    catch (Exception e)
    {
        Console.WriteLine($"number {i} Already exists EX{e.Message}");
        i++;
    }
}
await targetYouTubeCredential.RevokeTokenAsync(new CancellationToken());
