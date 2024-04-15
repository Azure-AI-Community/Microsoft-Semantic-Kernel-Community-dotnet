namespace AzureAI.Community.SK.PlugIn.Translation;

public interface ITranslationConnector
{
    Task<string?> TranslateText(string input);
}