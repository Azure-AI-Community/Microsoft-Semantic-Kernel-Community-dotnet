using System.Text;

namespace AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.Bing.News;

internal class BingNewsBuild
{

    public static string BuildUri(BingNewsParameter bingNewsParameter)
    {
        var queryBuilder = new StringBuilder();

        if (string.IsNullOrEmpty(bingNewsParameter.Category))
        {
            if (bingNewsParameter.Query != null)
                queryBuilder.Append(
                    $"https://api.bing.microsoft.com/v7.0/news/search?q={Uri.EscapeDataString(bingNewsParameter.Query)}");

            if (!string.IsNullOrEmpty(bingNewsParameter.SafeSearch))
            {
                queryBuilder.Append($"&safesearch={Uri.EscapeDataString(bingNewsParameter.SafeSearch)}");
            }

            if (!string.IsNullOrEmpty(bingNewsParameter.TextFormat))
            {
                queryBuilder.Append($"&textFormat={Uri.EscapeDataString(bingNewsParameter.TextFormat)}");
            }

            //if (!string.IsNullOrEmpty(bingNewsParameter.ResponseFilter))
            //{
            //    queryBuilder.Append($"&responseFilter={bingNewsParameter.ResponseFilter}");
            //}

            // Conditionally add parameters based on whether they are set and not empty
            if (!string.IsNullOrEmpty(bingNewsParameter.Category))
            {
                queryBuilder.Append($"&category={Uri.EscapeDataString(bingNewsParameter.Category)}");
            }

            if (!string.IsNullOrEmpty(bingNewsParameter.Freshness))
            {
                queryBuilder.Append($"&freshness={Uri.EscapeDataString(bingNewsParameter.Freshness)}");
            }

            if (!string.IsNullOrEmpty(bingNewsParameter.SortBy))
            {
                queryBuilder.Append($"&sortBy={Uri.EscapeDataString(bingNewsParameter.SortBy)}");
            }

            if (!string.IsNullOrEmpty(bingNewsParameter.Cc))
            {
                queryBuilder.Append($"&cc={Uri.EscapeDataString(bingNewsParameter.Cc)}");
            }
        }
        else
        {
            if (string.CompareOrdinal(bingNewsParameter.Category.ToLower(), BingNewsCategories.Top?.ToLower()) == 0)
            {
                queryBuilder.Append($"https://api.bing.microsoft.com/v7.0/news/search?q={string.Empty}");
            }
            else
            {
                queryBuilder.Append($"https://api.bing.microsoft.com/v7.0/news?&category={bingNewsParameter.Category}");
            }

            if (bingNewsParameter.Count > 0)
            {
                queryBuilder.Append($"&count={bingNewsParameter.Count}");
            }

            if (bingNewsParameter.Offset > 0)
            {
                queryBuilder.Append($"&offset={bingNewsParameter.Offset}");
            }

            if (!string.IsNullOrEmpty(bingNewsParameter.Market))
            {
                queryBuilder.Append($"&mkt={Uri.EscapeDataString(bingNewsParameter.Market)}");
            }

            //queryBuilder.Append($"&textDecorations={bingNewsParameter.TextDecorations.ToString().ToLower()}");
        }

        return queryBuilder.ToString();

    }
}