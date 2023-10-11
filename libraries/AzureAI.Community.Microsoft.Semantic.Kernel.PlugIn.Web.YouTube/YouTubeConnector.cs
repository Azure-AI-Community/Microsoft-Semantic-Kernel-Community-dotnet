using AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.YouTube.Search;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.SemanticKernel.Plugins.Web;
using Newtonsoft.Json;

namespace AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.YouTube
{
    public sealed class YouTubeConnector : IWebSearchEngineConnector
    {
        private readonly IYouTubeSearch youTubeSearch;
        private readonly ILogger logger;

        public YouTubeConnector(string apiKey, ILoggerFactory? loggerFactory = null,string channelId = "")
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey));
            }   

            youTubeSearch = new YouTubeSearch(apiKey, channelId);

            logger = loggerFactory is not null ? loggerFactory.CreateLogger(nameof(IYouTubeSearch)) : NullLogger.Instance;
        }


        public async Task<IEnumerable<string>> SearchAsync(string query, int count = 10, int offset = 0,
            CancellationToken cancellationToken = new CancellationToken())
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentNullException(nameof(query));
            }

            var search = await youTubeSearch.Search(query, count);

            if (string.IsNullOrEmpty(search))
            {
                return Enumerable.Empty<string>();
            }

            this.logger.LogTrace("Response content received: {Data}", search);

            search = search.Replace(": null", ": 0");

            List<YouTubeResult>? videoResult = JsonConvert.DeserializeObject<List<YouTubeResult>>(search);
            if (videoResult?.Count > 0)
            {
                List<string> urlList = new();
                foreach (var result in videoResult)
                {
                    if (result?.Id?.VideoId != null)
                    {
                        var id = result.Id.VideoId.ToString();
                        if (string.CompareOrdinal(id, "0") != 0)
                        {
                            urlList.Add($"https://www.youtube.com/watch?v={result.Id.VideoId.ToString()}");
                        }
                    }
                }

                return urlList?.Count > 0 ? urlList : Enumerable.Empty<string>();
            }

            return new[] { string.Empty };
            
        }
    }
}
