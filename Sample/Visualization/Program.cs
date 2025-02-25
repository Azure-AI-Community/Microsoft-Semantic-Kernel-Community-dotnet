// See https://aka.ms/new-console-template for more information

using AzureAI.Community.SK.Plugin.MermaidPlugin;
using AzureAI.Community.SK.Plugin.VisualizationPlugin;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

Console.WriteLine("Hello, World!");

string diagramDescription = @"
1. Vinoth says to Ramesh, 'Hello Ramesh, how are you?'
2. Ramesh replies to Vinoth, 'I am good, thanks!'
3. Vinoth says to Karthik, 'Hello Karthik, how are you?'
4. Karthik replies to Vinoth, 'I am good too!'
5. Ramesh asks Karthik, 'How are you, Karthik?'
6. Karthik replies to Ramesh, 'I am good, Ramesh!'
7. Ramesh says to Vinoth, 'Goodbye Vinoth!'
8. Vinoth says to Karthik, 'Goodbye Karthik!'
9. Karthik says to Ramesh, 'Goodbye Ramesh!'
";

Kernel kernel = Kernel.CreateBuilder()
    .AddAzureOpenAIChatCompletion("gpt-4o", "", "")
    .Build();


kernel.Plugins.AddFromType<VisualizationPlugin>(nameof(VisualizationPlugin));
kernel.Plugins.AddFromType<MermaidPlugin>(nameof(MermaidPlugin));


string prompt = $"create visualization if for the following steps? {diagramDescription}";

var openAIPromptSettings = new OpenAIPromptExecutionSettings()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

//Chat Completion Service
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();


//Chat History
var chatHistory = new ChatHistory();
chatHistory.AddSystemMessage("use avaliable plugin to get better results and send the compleate path information to gernmerate image");
chatHistory.AddUserMessage(prompt);

var result =
    await chatCompletionService.GetChatMessageContentAsync(chatHistory, openAIPromptSettings,
        kernel);

Console.WriteLine(result.Content);

Console.Read();