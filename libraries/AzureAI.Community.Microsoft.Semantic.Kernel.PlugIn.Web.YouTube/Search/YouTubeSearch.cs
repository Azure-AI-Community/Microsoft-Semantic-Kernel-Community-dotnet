using System.Text.Json;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.YouTube.Search;

internal class YouTubeSearch : IYouTubeSearch
{
    private readonly YouTubeService youtubeService;
    private string chennalId;
    public YouTubeSearch(string apiKey,string channelId = "")
    {
        if (string.IsNullOrEmpty(apiKey))
            throw new ArgumentNullException(nameof(apiKey));

        youtubeService= new YouTubeService(new BaseClientService.Initializer()
        {
            ApiKey = apiKey,
            ApplicationName = GetType().ToString(),
        });
        this.chennalId = channelId;
    }

    public async Task<string> Search(string keyWords,int count=10)
    {
        if (string.IsNullOrEmpty(keyWords))
            throw new ArgumentNullException(nameof(keyWords));

        string result;
        try
        {
            var searchListRequest = youtubeService.Search.List("snippet");

            if (!string.IsNullOrEmpty(chennalId) && !string.IsNullOrWhiteSpace(chennalId))
            {
                searchListRequest.ChannelId = chennalId;
            }

            searchListRequest.MaxResults = count;

            searchListRequest.Q = keyWords;

                var searchListResponse = await searchListRequest.ExecuteAsync();

                var options = new JsonSerializerOptions { WriteIndented = true };
                result = JsonSerializer.Serialize(searchListResponse.Items, options);
            
        }
        catch (Exception e)
        {
            result = string.Empty;
        }

        return result;
    }
}