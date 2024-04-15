using System.Text;

namespace AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Intent.Template;

static class PromptBuilder
{

    public const int MaxTokens = 1024;
    public const double Temperature = 0.1;
    public const double TopP = 0.5;

    public static string Build(params TemplateExamples[]? examples)
    {
        var promptTemplate = "Identify the intent and entities in the provided text and return in JSON";

        if (examples is not { Length: > 0 })
        {
            examples = GenerateTemplateExamples.CreateExamples();
        }

        var examplesTemplate = GenerateExample(examples);

        StringBuilder promptBuilder = new();

        promptBuilder.AppendLine(promptTemplate);
        promptBuilder.AppendLine(examplesTemplate);
        promptBuilder.AppendLine("+++++");
        promptBuilder.AppendLine("[Input]");
        promptBuilder.AppendLine("{{$INPUT}}");
        promptBuilder.AppendLine("[Output]");

        return promptBuilder.ToString();
    }

    private static string GenerateExample(params TemplateExamples[] examples)
    {
        StringBuilder concatenatedExamples = new StringBuilder();

        int Idx = 1;

        foreach (TemplateExamples example in examples)
        {
            var exampleString = "Example" + Idx++;
            concatenatedExamples.AppendLine(exampleString);
            concatenatedExamples.AppendLine("[Input]");
            concatenatedExamples.AppendLine(example.Input);
            concatenatedExamples.AppendLine("[Output]");
            concatenatedExamples.AppendLine(example.Output);
        }

        return concatenatedExamples.ToString();
    }

}