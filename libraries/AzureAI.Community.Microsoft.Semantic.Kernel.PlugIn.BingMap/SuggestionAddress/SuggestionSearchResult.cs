using Azure;
using Azure.Core.GeoJson;
using Azure.Maps.Search;
using Azure.Maps.Search.Models;

namespace AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.BingMap.SuggestionAddress
{
    public class SuggestionSearchResult : ISuggestionSearchResult
    {
        private readonly MapsSearchClient client;

        public SuggestionSearchResult(string bingMapsKey)
        {
            AzureKeyCredential credential = new(bingMapsKey);

            client = new MapsSearchClient(credential);
        }

        public async Task<List<Address>?> SearchSuggestionAsync(string suggestedLocationTypes, string location)
        {
            Response<SearchAddressResult> searchResult = await client.SearchAddressAsync(location);

            if (searchResult.Value.Results.Count < 0)
                return null;

            SearchAddressResultItem resultItem = searchResult.Value.Results[0];

            Response<SearchAddressResult> fuzzySearchResponse = await client.FuzzySearchAsync(suggestedLocationTypes,
                new FuzzySearchOptions
                {
                    Coordinates = new GeoPosition(resultItem.Position.Longitude, resultItem.Position.Latitude),
                    Language = SearchLanguage.EnglishUsa
                });


            if (fuzzySearchResponse.Value is not { Results: not null })
                return null;

            List<Address> addresses = new();

            foreach (SearchAddressResultItem result in fuzzySearchResponse.Value.Results)
            {
                addresses.Add(new()
                {
                    Name = result.PointOfInterest?.Name,
                    Latitude = result.Position.Latitude,
                    Longitude = result.Position.Longitude,
                    Location = result.Address.FreeformAddress
                });
            }

            return addresses;
        }
    }
}