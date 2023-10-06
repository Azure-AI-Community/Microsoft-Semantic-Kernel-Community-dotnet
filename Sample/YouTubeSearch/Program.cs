using AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.YouTube;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Skills.Web;

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
            
            var youtubeSkill = kernel.ImportSkill(new WebSearchEngineSkill(youTubeConnector), nameof(YouTubeConnector));

            var result = await kernel.RunAsync("Bot composer", youtubeSkill["search"]);

            string[] urls = result.Result.Replace("[", "").Replace("]", "").Split(',');

            int serialNumber = 1;
            foreach (string url in urls)
            {
                string trimmedUrl = url.Trim();
                Console.WriteLine($"{serialNumber++}: {trimmedUrl}");
            }

            Console.ReadKey();

        }
    }
}
