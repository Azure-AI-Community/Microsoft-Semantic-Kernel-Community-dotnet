namespace AzureAI.Community.SK.PlugIn.Web.BingMap.SuggestionAddress;

public interface ISuggestionSearchResult
{
    Task<List<Address>?> SearchSuggestionAsync(string suggestedLocationTypes, string location);
}