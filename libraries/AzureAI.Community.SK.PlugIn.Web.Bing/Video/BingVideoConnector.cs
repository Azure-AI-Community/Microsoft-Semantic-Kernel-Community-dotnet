using AzureAI.Community.SK.PlugIn.Web.Bing.Video.Search;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.SemanticKernel.Plugins.Web;
using Newtonsoft.Json;

namespace AzureAI.Community.SK.PlugIn.Web.Bing.Video
{

#pragma warning disable SKEXP0054
    public class BingVideoConnector : IWebSearchEngineConnector
    {
        private readonly string subscriptionKey;
        private readonly ILogger logger;
        private readonly BingVideoSearchParameters searchParameters;
        public BingVideoConnector(string subscriptionKey, BingVideoSearchParameters? videoSearchParameters = null, ILoggerFactory? loggerFactory = null)
        {
            if (string.IsNullOrEmpty(subscriptionKey))
                throw new ArgumentNullException(nameof(subscriptionKey));

            this.subscriptionKey = subscriptionKey;

            searchParameters = videoSearchParameters ?? new BingVideoSearchParameters() { Market = "en-IN", SafeSearch = BingSearchSafeSearchList.Moderate };

            logger = loggerFactory is not null ? loggerFactory.CreateLogger(typeof(BingVideoConnector)) : NullLogger.Instance;
        }

        async Task<HttpResponseMessage> MakeRequestAsync(string uriQuery)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            return await client.GetAsync(uriQuery);
        }


        public async Task<IEnumerable<string>> SearchAsync(string query, int count = 1, int offset = 0,
            CancellationToken cancellationToken = new CancellationToken())
        {
            if (count is <= 0 or >= 50)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, $"{nameof(count)} value must be greater than 0 and less than 50.");
            }

            searchParameters.Count = count;
            searchParameters.Offset = offset;
            searchParameters.SearchQuery = query;

            var uriQuery = BingVideoBuild.BuildUri(searchParameters);

            var response = await MakeRequestAsync(uriQuery);

            var search = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            if (string.IsNullOrEmpty(search))
            {
                return Enumerable.Empty<string>();
            }

            logger.LogTrace("Response content received: {Data}", search);

            search = search.Replace(": null", ": 0");

            Dictionary<string, object>? searchResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(search);

            var jToken = searchResponse?["value"] as Newtonsoft.Json.Linq.JToken;

            var result1 = jToken?.ToString();

            if (!string.IsNullOrEmpty(result1))
            {
                List<VideoInfo>? videos = JsonConvert.DeserializeObject<List<VideoInfo>>(result1);

                if (videos?.Count > 0)
                {
                    var urlList = videos.Select(result => result.ContentUrl).ToList();
                    return urlList.Count <= 0 ? Enumerable.Empty<string>() : urlList!;
                }
            }

            return Enumerable.Empty<string>();
        }
    }

}
