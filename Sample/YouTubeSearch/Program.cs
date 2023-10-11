using AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.YouTube;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.Web;

namespace YouTubeSearchSample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, Youtube Connector for Microsoft Semantic Kernel");

            string apiKey = "";
            string youTubeChannelId = "";

            var youTubeConnector = new YouTubeConnector(apiKey: apiKey,channelId: youTubeChannelId);

            IKernel kernel = Kernel.Builder.Build();

            var youtubeSkill = kernel.ImportFunctions(new WebSearchEnginePlugin(youTubeConnector), nameof(YouTubeConnector));

            var result = await kernel.RunAsync("Bot composer", youtubeSkill["search"]);

            var resultUrl = result?.GetValue<string>();

            string[]? urls = resultUrl?.Replace("[", "").Replace("]", "").Split(',');

            int serialNumber = 1;
            if (urls != null)
            {
                foreach (string url in urls)
                {
                    string trimmedUrl = url.Trim();
                    Console.WriteLine($"{serialNumber++}: {trimmedUrl}");
                }
            }

            Console.ReadKey();

        }
    }
}
