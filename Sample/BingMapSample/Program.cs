using AzureAI.Community.SK.PlugIn.Web.BingMap.SuggestionAddress;
using Microsoft.SemanticKernel;

namespace BingMapSample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello,Bing Map");

            var kernel = Kernel.CreateBuilder()
                .AddAzureOpenAIChatCompletion(Config.DeploymentOrModelId, Config.Endpoint, Config.ApiKey)
                .Build();

            FunctionResult result;

            //Add the location search plugin 
            var suggestionAddressPlugin = new SuggestionAddressPlugin(Config.BingMapKey);
            
            kernel.Plugins.AddFromObject(suggestionAddressPlugin);
            

            //Create kernel function
            var suggestionKernelFunction =
                kernel.Plugins.GetFunction(nameof(SuggestionAddressPlugin), "SearchSuggestion");

            //Trigger invoke function
            var result1 = await kernel.InvokeAsync(suggestionKernelFunction, new KernelArguments
            {
                { "suggestion", "restaurants" },
                { "city", "Thanjavur" }
            });

            Console.WriteLine(result1.GetValue<string>());

            Console.Read();
        }
    }
}
