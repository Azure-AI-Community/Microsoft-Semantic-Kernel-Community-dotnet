using Microsoft.CognitiveServices.Speech;

namespace AzureAI.Community.Microsoft.Semantic.Kernel.Speech;

public interface ISpeechToText
{
    bool Build();
    Task<string> StartToSpeech();

    SpeechConfig SpeechConfig { get; }
}