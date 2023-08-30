using System.ComponentModel;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.SkillDefinition;

namespace AzureAI.Community.Microsoft.Semantic.Kernel.Speech.Plugin;

public sealed class SpeechPlugin
{
    private readonly ISpeechToText speechToText;
    public SpeechPlugin(ISpeechToText speechToText)
    {
        this.speechToText = speechToText ?? throw new ArgumentNullException(nameof(speechToText));
    }

    [SKFunction, Description("Utilizes a speech recognition engine to process user speech input and returns text")]
    public async Task<string> ListenSpeechVoice(SKContext context)
    {
        var result = await speechToText.StartToSpeech();
        return result;
    }
}