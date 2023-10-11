using AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.Bing.Video;
using Google.Apis.CustomSearchAPI.v1.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.SemanticKernel.Plugins.Web;

using Newtonsoft.Json;

namespace AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.Bing.News;

public class BingNewsConnector : IWebSearchEngineConnector
{
    private readonly string subscriptionKey;
    private readonly ILogger logger;
    private readonly BingNewsParameter searchParameters;

    public BingNewsConnector(string subscriptionKey, BingNewsParameter? bingNewsParameter,
        ILoggerFactory? loggerFactory = null)
    {
        if (string.IsNullOrEmpty(subscriptionKey))
            throw new ArgumentNullException(nameof(subscriptionKey));

        this.subscriptionKey = subscriptionKey;

        searchParameters = bingNewsParameter ?? new BingNewsParameter()
            { Market = "en-IN", SafeSearch = BingSearchSafeSearchList.Moderate };

        logger = loggerFactory is not null
            ? loggerFactory.CreateLogger(typeof(BingVideoConnector))
            : NullLogger.Instance;
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
            throw new ArgumentOutOfRangeException(nameof(count), count,
                $"{nameof(count)} value must be greater than 0 and less than 50.");
        }

        searchParameters.Count = count;
        searchParameters.Offset = offset;
        searchParameters.Query = query;

        var uriQuery = BingNewsBuild.BuildUri(searchParameters);

        var response = await MakeRequestAsync(uriQuery);

        var search = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        if (string.IsNullOrEmpty(search))
        {
            return Enumerable.Empty<string>();
        }

        Dictionary<string, object>? searchResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(search);

        if (searchResponse == null) 
            return Enumerable.Empty<string>();

        string? jsonResult= string.Empty;

        if (string.IsNullOrEmpty(searchParameters.Category))
        {
            if (searchResponse.Keys.Any(newsItem => newsItem == "news"))
            {
                //logger.LogTrace("Response content received: {Data}", search);

                var news = searchResponse["news"] as Newtonsoft.Json.Linq.JToken;
                var jToken = news?["value"];

                jsonResult = jToken?.ToString();
            }
        }
        else
        {
            var news = searchResponse["value"] as Newtonsoft.Json.Linq.JToken;

            jsonResult = news?.ToString();
        }

        if (!string.IsNullOrEmpty(jsonResult))
        {
            List<BingNewsResult>? newsResults = JsonConvert.DeserializeObject<List<BingNewsResult>>(jsonResult);

            if (newsResults?.Count > 0)
            {
                var urlList = newsResults.Select(result => result.url).ToList();
                return urlList?.Count > 0 ? urlList! : Enumerable.Empty<string>();
            }
        }

        return Enumerable.Empty<string>();
    }
}