using AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.Bing.News;
using AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.Bing.Video;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.Web;

namespace BingSearchSample
{
#pragma warning disable SKEXP0054
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello,Bing Search News - Video");

            var kernel = Kernel.CreateBuilder()
                .AddAzureOpenAIChatCompletion(Config.DeploymentOrModelId, Config.Endpoint, Config.ApiKey)
                .Build();

            FunctionResult result;

            bool includeCoordinates = true;

            while (true)
            {
                Console.WriteLine("1. Bing Video Search");
                Console.WriteLine("2. Bing News Search");
                Console.WriteLine("3. Bing Map Location Search");
                Console.WriteLine("4. Exits");


                Console.WriteLine("\nEnter your choice:");
                SearchType searchType = (SearchType)Convert.ToInt32(Console.ReadLine());

                Console.WriteLine();
                Console.WriteLine("Type the keyword to search");
                string keyword = Console.ReadLine() ?? throw new ArgumentNullException("Console.ReadLine()");

                WebSearchEnginePlugin? plugin = null;

                if (searchType == SearchType.BingVideos)
                {
                    BingVideoSearchParameters videoSearchParameters = new()
                    { Market = "en-IN", SafeSearch = BingSearchSafeSearchList.Moderate };

                    var bingConnector = new BingVideoConnector(Config.BingVideoSubscriptionKey, videoSearchParameters);

                    plugin = new WebSearchEnginePlugin(bingConnector);

                }
                else if (searchType == SearchType.BingNews)
                {
                    BingNewsParameter bingNewsParameter = new()
                    {
                        Market = "en-IN",
                        SafeSearch = BingSearchSafeSearchList.Moderate,
                        Category = BingNewsCategories.Business
                    };

                    var bingConnector = new BingNewsConnector(Config.BingNewsSubscriptionKey, bingNewsParameter);

                    plugin = new WebSearchEnginePlugin(bingConnector);
                }
                else
                {
                    Console.WriteLine("Bye....");
                    break;
                }

                kernel.ImportPluginFromObject(plugin, nameof(WebSearchEnginePlugin));

                var weatherKernelFunction =
                    kernel.Plugins.GetFunction(nameof(WebSearchEnginePlugin), "search");

                //KernelArguments 
                var kernelArguments = new KernelArguments
                {
                    { "query", keyword }
                };

                //Invoke the kernel function
                result = await kernel.InvokeAsync(weatherKernelFunction, kernelArguments);

                var resultUrl = result?.GetValue<string>();

                Console.WriteLine("Result: \n");

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

                Console.WriteLine("\n\n");
                Console.Read();
            }
        }
    }
}
