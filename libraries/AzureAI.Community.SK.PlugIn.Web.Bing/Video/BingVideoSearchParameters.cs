namespace AzureAI.Community.SK.PlugIn.Web.Bing.Video;

public class BingVideoSearchParameters
{
    public required string Market { get; set; }
    public int? Count { get; internal set; }
    public int? Offset { get; internal set; }
    public string? VideoId { get; set; }
    public required string SafeSearch { get; set; }
    public string? SearchQuery { get; internal set; }

}

public static class BingSearchSafeSearchList
{
    public const string Moderate = "Moderate";
    public const string Off = "Off";
    public const string Strict = "Strict";
}