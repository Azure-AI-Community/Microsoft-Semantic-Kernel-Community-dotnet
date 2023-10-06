namespace AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.YouTube;

internal class Thumbnails
{
    public Default Default__ { get; set; }
    public High High { get; set; }
    public object Maxres { get; set; }
    public Medium Medium { get; set; }
    public object Standard { get; set; }
    public object ETag { get; set; }
}

internal class Default
{
    public int Height { get; set; }
    public string Url { get; set; }
    public int Width { get; set; }
    public object ETag { get; set; }
}

internal class High
{
    public int Height { get; set; }
    public string Url { get; set; }
    public int Width { get; set; }
    public object ETag { get; set; }
}

internal class Medium
{
    public int Height { get; set; }
    public string Url { get; set; }
    public int Width { get; set; }
    public object ETag { get; set; }
}

internal class Snippet
{
    public string ChannelId { get; set; }
    public string ChannelTitle { get; set; }
    public string Description { get; set; }
    public string LiveBroadcastContent { get; set; }
    public string PublishedAtRaw { get; set; }
    public string PublishedAtDateTimeOffset { get; set; }
    public string PublishedAt { get; set; }
    public Thumbnails Thumbnails { get; set; }
    public string Title { get; set; }
    public object ETag { get; set; }
}

internal class Id
{
    public object ChannelId { get; set; }
    public string Kind { get; set; }
    public string PlaylistId { get; set; }
    public object VideoId { get; set; }
    public object ETag { get; set; }
}

internal class YoTubeResult
{
    public string ETag { get; set; }
    public Id Id { get; set; }
    public string Kind { get; set; }
    public Snippet Snippet { get; set; }
}
