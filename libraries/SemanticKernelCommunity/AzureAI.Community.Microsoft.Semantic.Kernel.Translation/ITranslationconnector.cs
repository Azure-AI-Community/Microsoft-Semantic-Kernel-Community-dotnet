namespace AzureAI.Community.Microsoft.Semantic.Kernel.Translation.Text;

public interface ITranslationConnector
{
    Task<string?> TranslateText(string input);
}