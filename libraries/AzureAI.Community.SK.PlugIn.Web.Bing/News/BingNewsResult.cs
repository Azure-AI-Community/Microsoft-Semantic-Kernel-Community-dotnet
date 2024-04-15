namespace AzureAI.Community.SK.PlugIn.Web.Bing.News;

public class ContractualRule
{
    public string? _type { get; set; }
    public string? text { get; set; }
}

public class Thumbnail
{
    public string? contentUrl { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class Image
{
    public string? contentUrl { get; set; }
    public Thumbnail? thumbnail { get; set; }
}

public class About
{
    public string? readLink { get; set; }
    public string? name { get; set; }
}

public class Provider
{
    public string? _type { get; set; }
    public string? name { get; set; }
    public Image? image { get; set; }
}

public class BingNewsResult
{
    public List<ContractualRule>? contractualRules { get; set; }
    public string? name { get; set; }
    public string? url { get; set; }
    public Image? image { get; set; }
    public string? description { get; set; }
    public List<About>? about { get; set; }
    public List<Provider>? provider { get; set; }
    public DateTime? datePublished { get; set; }
}

public class BingNewsCategory
{
    public string? name { get; set; }
    public string? url { get; set; }
    public Image? image { get; set; }
    public string? description { get; set; }
    public List<Provider>? provider { get; set; }
    public DateTime? datePublished { get; set; }
}
