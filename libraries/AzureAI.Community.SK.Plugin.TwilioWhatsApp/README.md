[Azure AI Community GitHub](https://github.com/Azure-AI-Community)

# AzureAI Community Semantic Kernel Plugin - Twilio WhatsApp Plugin

The `AzureAI.Community.SK.Plugin.TwilioWhatsApp` plugin for Microsoft Semantic Kernel (SK) allows seamless integration with Twilio's WhatsApp API, enabling you to send WhatsApp messages directly from your SK-powered applications. By using this plugin, you can easily send messages to WhatsApp numbers through Twilio's API, integrating WhatsApp messaging into your workflows with minimal effort.


## Prerequisites

Before using the WhatsAppServicePlugin, ensure you meet the following requirements:

1. **Twilio Account**  
   You’ll need a Twilio account to access the Twilio API for WhatsApp messaging. If you don’t have one, you can sign up at Twilio.

2. **Twilio API Credentials**  
   To authenticate with Twilio’s API, you must have the following credentials:
   - **Account SID**: Your Twilio Account SID, available in the Twilio Console.
   - **Auth Token**: Your Twilio Auth Token, also available in the console.
   You can locate these credentials in the "Account Info" section of your Twilio dashboard.

3. **Twilio WhatsApp Sandbox Number**  
   To send WhatsApp messages, you’ll need to set up Twilio's WhatsApp sandbox. After configuring your sandbox in the Twilio console, you'll receive a sandbox number to send messages from.
   - **From Number**: The number from which messages will be sent (usually formatted as `whatsapp:+344324234` for sandbox usage).

Before using the `AzureAI.Community.SK.Plugin.TwilioWhatsApp`, ensure the following are in place:
- Azure AI Semantic Kernel installed.
- Access to the Semantic Kernel Plugin system.

## Installing the Plugin

To install the `AzureAI.Community.SK.Plugin.TwilioWhatsApp`, simply add it to your project via the kernel.

### Example Code:

```csharp
// Add the AzureAI.Community.SK.Plugin.TwilioWhatsApp to the Kernel
kernel.Plugins.AddFromObject(new WhatsAppServicePlugin(accountSid, authToken, fromPhoneNo));

// Send a WhatsApp message
string prompt = $"Please inform {toPhoneNo} that the office will be closed tomorrow";
var openAiPromptSettings = new OpenAIPromptExecutionSettings()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

// Chat Completion Service
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

// Chat History
var chatHistory = new ChatHistory();
chatHistory.AddSystemMessage("You are an AI assistant.");
chatHistory.AddUserMessage(prompt);

var result = await chatCompletionService.GetChatMessageContentAsync(chatHistory, openAiPromptSettings, kernel);

Console.WriteLine(result.Content);
