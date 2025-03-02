using AzureAI.Community.SK.Plugin.TwilioWhatsApp;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace TwilioWhatsApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Azure OpenAI Chat Completion Service
            string deploymentOrModelId = "gpt-4o";
            string endpoint = "";
            string apiKey = "";

            // Twilio WhatsApp Service
            var accountSid = "";
            var authToken = "";
            var fromPhoneNo = "";
            var toPhoneNo = "";

            Kernel kernel = Kernel.CreateBuilder()
                .AddAzureOpenAIChatCompletion(deploymentOrModelId, endpoint, apiKey)
                .Build();

            kernel.Plugins.AddFromObject(new WhatsAppServicePlugin(accountSid, authToken, fromPhoneNo));
            
            string prompt = $"Please inform {toPhoneNo} that the office will be closed tomorrow";

            var openAiPromptSettings = new OpenAIPromptExecutionSettings()
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };

            //Chat Completion Service
            var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();


            //Chat History
            var chatHistory = new ChatHistory();
            chatHistory.AddSystemMessage("You are an AI assistant.");
            chatHistory.AddUserMessage(prompt);

            var result =
                await chatCompletionService.GetChatMessageContentAsync(chatHistory, openAiPromptSettings,
                    kernel);

            Console.WriteLine(result.Content);
            Console.WriteLine("Done !!!");
            Console.Read();
        }
    }
}
