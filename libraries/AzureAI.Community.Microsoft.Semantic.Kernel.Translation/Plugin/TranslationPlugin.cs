using System.ComponentModel;
using AzureAI.Community.Microsoft.Semantic.Kernel.Translation.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Orchestration;


namespace AzureAI.Community.Microsoft.Semantic.Kernel.Translation.Plugin;

public sealed class TranslationPlugin
{
    public static class Parameters
    {
        public const string Input = "input";
    }

    private readonly ITranslationConnector translationConnector;
    private readonly ILogger logger;

    public TranslationPlugin(ITranslationConnector connector, ILoggerFactory? loggerFactory = null)
    {
        translationConnector = connector ?? throw new ArgumentNullException(nameof(connector));
        logger = loggerFactory is not null ? loggerFactory.CreateLogger(nameof(TranslationText)) : NullLogger.Instance;
    }

    /// <summary>
    /// Translating text while automatically detecting the source language of the input.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    [SKFunction, Description("Translating source input text into the target languages")]
    public async Task<string> Translate(string input, SKContext context)
    {

        if (string.IsNullOrEmpty(input))
            return input;

        logger.LogDebug(" Input:{0} for translate ", input);

        var translate = await translationConnector.TranslateText(input);

        if (string.IsNullOrEmpty(translate))
            return input;

        context.Variables.Set("Translation", translate);

        logger.LogDebug("Translated text {0} ", translate);

        return translate;
    }

}