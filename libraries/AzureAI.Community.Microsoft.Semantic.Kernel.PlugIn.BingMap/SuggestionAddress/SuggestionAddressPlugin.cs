using System.ComponentModel;
using Microsoft.SemanticKernel;
using Newtonsoft.Json;

namespace AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.BingMap.SuggestionAddress;

public class SuggestionAddressPlugin
{
    readonly ISuggestionSearchResult suggestionSearchResult;

    public SuggestionAddressPlugin(string bingMapKey)
    {
        if (string.IsNullOrEmpty(bingMapKey))
        {
            throw new ArgumentNullException(nameof(bingMapKey));
        }

        bingMapKey = bingMapKey.Trim();

        if (string.IsNullOrEmpty(bingMapKey))
            throw new ArgumentNullException(nameof(bingMapKey));

        suggestionSearchResult = new SuggestionSearchResult(bingMapKey);
    }

    [KernelFunction, Description("Find the suggestion based on the city")]
    public async Task<string> SearchSuggestionAsync([Description("suggestion")] string suggestion, [Description("city name")] string city)
    {
        if (string.IsNullOrEmpty(suggestion))
        {
            throw new ArgumentNullException(nameof(suggestion));
        }

        if (string.IsNullOrEmpty(city))
        {
            throw new ArgumentNullException(nameof(city));
        }

        var addresses = await suggestionSearchResult.SearchSuggestionAsync(suggestion, city);

        if (addresses == null || addresses.Count < 0)
        {
            return string.Empty;
        }

        var result = JsonConvert.SerializeObject(addresses, Formatting.Indented);

        return result;
    }
}