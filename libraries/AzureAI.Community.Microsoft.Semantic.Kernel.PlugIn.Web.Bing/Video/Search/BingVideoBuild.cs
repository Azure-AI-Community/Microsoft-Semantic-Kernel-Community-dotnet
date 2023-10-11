using System.Text;

namespace AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.Bing.Video.Search;

internal class BingVideoBuild
{
    private static readonly string BaseUri = "https://api.bing.microsoft.com/v7.0/videos/search";
    private const string QueryParameter = "?q=";  // Required
    private const string MktParameter = "&mkt=";  // Strongly suggested
    private const string CountParameter = "&count=";
    private const string OffsetParameter = "&offset=";
    private const string IdParameter = "&id=";
    private const string SafeSearchParameter = "&safeSearch=";

    public static string BuildUri(BingVideoSearchParameters searchParameters)
    {

        if (searchParameters == null)
            throw new ArgumentNullException(nameof(searchParameters));

        if (string.IsNullOrEmpty(searchParameters.SearchQuery))
            throw new ArgumentNullException(nameof(searchParameters.SearchQuery));

        StringBuilder uriQueryBuilder = new StringBuilder(BaseUri);

        // Append the initial query parameter
        uriQueryBuilder.Append(QueryParameter);
        uriQueryBuilder.Append(Uri.EscapeDataString(searchParameters.SearchQuery));

        if (!string.IsNullOrEmpty(searchParameters.Market))
        {
            // Append the market parameter
            uriQueryBuilder.Append(MktParameter);
            uriQueryBuilder.Append(searchParameters.Market);
        }

        if (searchParameters.Count.HasValue)
        {
            // Append the count parameter if it has a value
            uriQueryBuilder.Append(CountParameter);
            uriQueryBuilder.Append(searchParameters.Count.Value.ToString());
        }

        if (searchParameters.Offset.HasValue)
        {
            // Append the offset parameter if it has a value
            uriQueryBuilder.Append(OffsetParameter);
            uriQueryBuilder.Append(searchParameters.Offset.Value.ToString());
        }

        if (!string.IsNullOrEmpty(searchParameters.VideoId))
        {
            // Append the videoId parameter
            uriQueryBuilder.Append(IdParameter);
            uriQueryBuilder.Append(searchParameters.VideoId);
        }

        if (!string.IsNullOrEmpty(searchParameters.SafeSearch))
        {
            // Append the safeSearch parameter
            uriQueryBuilder.Append(SafeSearchParameter);
            uriQueryBuilder.Append(searchParameters.SafeSearch);
        }

        // The uriQuery variable now contains the complete query string
        return uriQueryBuilder.ToString();
    }
}