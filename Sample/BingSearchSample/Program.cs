using AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.Bing.News;
using AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.Bing.Video;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.Plugins.Web;

namespace BingSearchSample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello,Bing Search");

            IKernel kernel = Kernel.Builder.Build();
            KernelResult? result = null;

            string bingVideoSubscriptionKey = "";
            string bingNewsSubscriptionKey = "";

            while (true)
            { 
                Console.WriteLine("1. Bing Video Search");
                Console.WriteLine("2. Bing News Search");
                Console.WriteLine("3. Exits");

                
                Console.WriteLine("\nEnter your choice:");
                SearchType searchType = (SearchType)Convert.ToInt32(Console.ReadLine());

                Console.WriteLine();
                Console.WriteLine("Type the keyword to search");
                string keyword = Console.ReadLine() ?? throw new ArgumentNullException("Console.ReadLine()");

                if (searchType == SearchType.BingVideos)
                {
                    BingVideoSearchParameters videoSearchParameters = new BingVideoSearchParameters()
                        { Market = "en-IN", SafeSearch = BingSearchSafeSearchList.Moderate };

                    var bingConnector = new BingVideoConnector(bingVideoSubscriptionKey, videoSearchParameters);

                    WebSearchEnginePlugin plugin = new WebSearchEnginePlugin(bingConnector);

                    var bingVideoSkill = kernel.ImportFunctions(plugin, "search");

                    result = await kernel.RunAsync(keyword, bingVideoSkill["search"]);

                }
                else if (searchType == SearchType.BingNews)
                {
                    BingNewsParameter bingNewsParameter = new BingNewsParameter
                    {
                        Market = "en-IN", SafeSearch = BingSearchSafeSearchList.Moderate,
                        Category = BingNewsCategories.Top
                    };

                    var bingConnector = new BingNewsConnector(bingNewsSubscriptionKey, bingNewsParameter);

                    var bingNewsSkill = kernel.ImportFunctions(new WebSearchEnginePlugin(bingConnector),
                        nameof(BingNewsParameter));

                    result = await kernel.RunAsync(keyword, bingNewsSkill["search"]);

                }
                else
                {
                    Console.WriteLine("Bye....");
                    break;
                }

                Console.WriteLine("Result: \n");
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

                Console.WriteLine("\n\n");
            }
        }
    }

    public enum SearchType
    {
        None = 0,
        BingVideos = 1,
        BingNews = 2,
    }
}
