namespace AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.YouTube.Search;

internal interface IYouTubeSearch
{
    Task<string> Search(string keyWords,int count);
}