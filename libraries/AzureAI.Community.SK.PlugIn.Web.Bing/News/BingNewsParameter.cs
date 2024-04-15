namespace AzureAI.Community.SK.PlugIn.Web.Bing.News;

public class BingNewsParameter
{
    public string? Query { get; internal set; }
    public int Count { get; internal set; } = 10;
    public int Offset { get; internal set; } = 0;
    public string? Market { get; set; }
    public string? Category { get; set; } = BingNewsCategories.Query;
    public string? SafeSearch { get; set; } 
    public string? Freshness { get; set; }
    public string? SortBy { get; set; }
    public string? Cc { get; set; }
    public bool TextDecorations { get; set; }
    public string? TextFormat { get; set; } 
    
}

public static class BingNewsSortBy
{
    public const string? Date = "Date";
    public const string? Relevance = "Relevance";
}

public static class BingNewsFreshness
{
    public const string? Day = "Day";
    public const string? Week = "Week";
    public const string? Month = "Month";
    public const string? All = "All";
}

public static class BingNewsTextFormat
{
    public const string? Raw = "Raw";
    public const string? Html = "Html";
}


public static class BingNewsCategories
{
    public const string? Query = "";
    public const string? Top = "Top";
    public const string? World = "World";
    public const string? Business = "Business";
    public const string? Entertainment = "Entertainment";
    public const string? Health = "Health";
    public const string? Politics = "Politics";
    public const string? ScienceAndTechnology = "ScienceAndTechnology";
    public const string? Sports = "Sports";
}
