using Microsoft.CognitiveServices.Speech;

namespace AzureAI.Community.SK.PlugIn.Speech;

public interface ISpeechToText
{
    bool Build();
    Task<string> StartToSpeech();

    SpeechConfig SpeechConfig { get; }
}