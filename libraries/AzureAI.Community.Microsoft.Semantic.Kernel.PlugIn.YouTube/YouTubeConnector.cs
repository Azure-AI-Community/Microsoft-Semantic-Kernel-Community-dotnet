using AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.YouTube.Search;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.SemanticKernel.Skills.Web;
using Newtonsoft.Json;

namespace AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.YouTube
{
    public sealed class YouTubeConnector : IWebSearchEngineConnector
    {
        private readonly IYouTubeSearch youTubeSearch;
        private readonly ILogger logger;

        public YouTubeConnector(string apiKey, ILoggerFactory? loggerFactory = null)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey));
            }   

            youTubeSearch = new YouTubeSearch(apiKey);

            logger = loggerFactory is not null ? loggerFactory.CreateLogger(nameof(IYouTubeSearch)) : NullLogger.Instance;
        }


        public async Task<IEnumerable<string>> SearchAsync(string query, int count = 1, int offset = 0,
            CancellationToken cancellationToken = new CancellationToken())
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentNullException(nameof(query));
            }

            var search = await youTubeSearch.Search(query,count);

            if (string.IsNullOrEmpty(search))
            {
                return new[] { string.Empty };
            }

            this.logger.LogTrace("Response content received: {Data}", search);

            List<YoTubeResult> videoResult = JsonConvert.DeserializeObject<List<YoTubeResult>>(search);

            if (videoResult?.Count > 0)
            {
                List<string> urlList = new();
                foreach (var result in videoResult)
                {
                    if (result?.Id?.VideoId != null)
                    {
                        urlList.Add($"https://www.youtube.com/watch?v={result.Id.VideoId.ToString()}");
                    }
                }
                return urlList?.Count <= 0 ? Enumerable.Empty<string>() : urlList;
            }

            return new[] { string.Empty };
            
        }
    }
}
