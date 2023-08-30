using Azure;
using Azure.AI.Translation.Text;
using AzureAI.Community.Microsoft.Semantic.Kernel.Translation.Text;

namespace AzureAI.Community.Microsoft.Semantic.Kernel.Translation
{
    public class TranslationText : ITranslationConnector
    {
        private readonly TextTranslationClient textTranslationClient;
        private readonly string targetLanguage;

        public TranslationText(string key, string targetLanguage, string region = "")
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            if (string.IsNullOrEmpty(targetLanguage))
                throw new ArgumentNullException(nameof(targetLanguage));

            textTranslationClient = new TextTranslationClient(new AzureKeyCredential(key), region: region);

            this.targetLanguage = targetLanguage;
        }

        public async Task<string?> TranslateText(string input)
        {
            return await Translate(input);
        }

        private async Task<string?> Translate(string input)
        {
            string? translateText = string.Empty;

            if (string.IsNullOrEmpty(input))
                return input;

            try
            {
                Response<IReadOnlyList<TranslatedTextItem>>? response = await textTranslationClient.TranslateAsync(targetLanguage, input);

                if (response != null)
                {
                    IReadOnlyList<TranslatedTextItem?> translations = response.Value;
                    if (translations.Count > 0)
                    {
                        TranslatedTextItem? translation = translations.FirstOrDefault();
                        translateText = translation?.Translations?.FirstOrDefault()?.Text;
                    }
                }
            }
            catch (RequestFailedException e)
            {
                translateText = input;
            }

            return translateText;
        }
    }
}
