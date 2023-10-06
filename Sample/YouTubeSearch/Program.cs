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

            var youTubeConnector = new YouTubeConnector("youtube-api-key");

            IKernel kernel = Kernel.Builder.Build();
            
            var youtubeSkill = kernel.ImportSkill(new WebSearchEngineSkill(youTubeConnector), nameof(YouTubeConnector));

            var result = await kernel.RunAsync("Bot composer", youtubeSkill["search"]);

            Console.WriteLine(result);

            Console.ReadKey();

        }
    }
}
