using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace AzureAI.Community.Microsoft.Semantic.Kernel.Speech
{
    public class SpeechToText : ISpeechToText
    {
        public SpeechConfig SpeechConfig { get; }

        private SpeechRecognizer? speechRecognizer;
        public SpeechToText(string key, string region)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            if (string.IsNullOrEmpty(region))
                throw new ArgumentNullException(nameof(region));

            SpeechConfig = SpeechConfig.FromSubscription(key, region);

            speechRecognizer = null;
        }

        public bool Build()
        {
            bool result = false;
            try
            {
                var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
                speechRecognizer = new SpeechRecognizer(SpeechConfig, audioConfig);
                result = true;
            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }

        public async Task<string> StartToSpeech()
        {
            if (speechRecognizer != null)
            {
                var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();

                if (speechRecognitionResult is { Reason: ResultReason.RecognizedSpeech })
                {
                    return speechRecognitionResult.Text;
                }
            }
            return string.Empty;
        }

    }
}
