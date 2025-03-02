// See https://aka.ms/new-console-template for more information

using AzureAI.Community.SK.Plugin.MermaidPlugin;
using AzureAI.Community.SK.Plugin.VisualizationPlugin;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

Console.WriteLine("Hello, World!");


//Azure OpenAI Chat Completion Service
//Azure OpenAI Chat Completion Service
string deploymentOrModelId = "gpt-4o";
string endpoint = "";
string apiKey = "";

string diagramDescription = @"
The decision starts with a question: Is it sunny? If the answer is yes, you choose to go for a walk and enjoy it.
If the answer is no, you decide to stay indoors and read a book. Both activities lead to the same conclusion: the end of the day. 
";

Kernel kernel = Kernel.CreateBuilder()
    .AddAzureOpenAIChatCompletion(deploymentOrModelId,endpoint,apiKey)
    .Build();


//kernel.Plugins.AddFromType<VisualizationPlugin>(nameof(VisualizationPlugin));
kernel.Plugins.AddFromType<MermaidPlugin>(nameof(MermaidPlugin));


string prompt = $"create visualization if for the following steps? {diagramDescription}";
prompt = $"create Mermaid if for the following steps? {diagramDescription}";

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