namespace AzureAI.Community.SK.PlugIn.Web.YouTube.Search;

internal interface IYouTubeSearch
{
    Task<string> Search(string keyWords,int count);
}