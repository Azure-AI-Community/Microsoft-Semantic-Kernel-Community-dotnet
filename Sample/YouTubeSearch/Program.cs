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

            var youTubeConnector = new YouTubeConnector(apiKey: Config.YoutubeKey, channelId: Config.YouTubeChannelKey);

            var kernel = Kernel.CreateBuilder()
                .AddAzureOpenAIChatCompletion(Config.DeploymentOrModelId, Config.Endpoint, Config.ApiKey)
                .Build();

#pragma warning disable SKEXP0054
            var youtubeSkill =
                kernel.ImportPluginFromObject(new WebSearchEnginePlugin(youTubeConnector), nameof(YouTubeConnector));

            KernelArguments kernelArguments = new() { { "query", "Microsoft Bot Composer" } };

            var result = await kernel.InvokeAsync(youtubeSkill["search"], kernelArguments);

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
