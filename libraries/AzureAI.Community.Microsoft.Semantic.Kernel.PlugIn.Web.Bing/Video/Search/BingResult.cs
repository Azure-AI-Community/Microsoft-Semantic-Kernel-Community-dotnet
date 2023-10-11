using Newtonsoft.Json;

namespace AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.Bing.Video.Search;

public class Publisher
{
    [JsonProperty("name")]
    public string? Name { get; set; }
}

public class Creator
{
    [JsonProperty("name")]
    public string? Name { get; set; }
}

public class Thumbnail
{
    [JsonProperty("width")]
    public int Width { get; set; }

    [JsonProperty("height")]
    public int Height { get; set; }
}

public class VideoInfo
{
    [JsonProperty("webSearchUrl")]
    public string? WebSearchUrl { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("description")]
    public string? Description { get; set; }

    [JsonProperty("thumbnailUrl")]
    public string? ThumbnailUrl { get; set; }

    [JsonProperty("datePublished")]
    public DateTime? DatePublished { get; set; }

    [JsonProperty("publisher")]
    public List<Publisher>? Publisher { get; set; }

    [JsonProperty("creator")]
    public Creator? Creator { get; set; }

    [JsonProperty("isAccessibleForFree")]
    public bool IsAccessibleForFree { get; set; }

    [JsonProperty("isFamilyFriendly")]
    public bool IsFamilyFriendly { get; set; }

    [JsonProperty("contentUrl")]
    public string? ContentUrl { get; set; }

    [JsonProperty("hostPageUrl")]
    public string? HostPageUrl { get; set; }

    [JsonProperty("encodingFormat")]
    public string? EncodingFormat { get; set; }

    [JsonProperty("hostPageDisplayUrl")]
    public string? HostPageDisplayUrl { get; set; }

    [JsonProperty("width")]
    public int Width { get; set; }

    [JsonProperty("height")]
    public int Height { get; set; }

    [JsonProperty("duration")]
    public string? Duration { get; set; }

    [JsonProperty("motionThumbnailUrl")]
    public string? MotionThumbnailUrl { get; set; }

    [JsonProperty("embedHtml")]
    public string? EmbedHtml { get; set; }

    [JsonProperty("allowHttpsEmbed")]
    public bool AllowHttpsEmbed { get; set; }

    [JsonProperty("viewCount")]
    public int ViewCount { get; set; }

    [JsonProperty("thumbnail")]
    public Thumbnail? Thumbnail { get; set; }

    [JsonProperty("videoId")]
    public string? VideoId { get; set; }

    [JsonProperty("allowMobileEmbed")]
    public bool AllowMobileEmbed { get; set; }

    [JsonProperty("isSuperfresh")]
    public bool IsSuperfresh { get; set; }
}

public class Root
{
    [JsonProperty("videoInfo")]
    public VideoInfo? VideoInfo { get; set; }
}