using System.ComponentModel;
using AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Intent.Template;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.SkillDefinition;

namespace AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Intent;

public class IntentPlugIn
{
    private readonly ISKFunction intentSkFunction;

    public IntentPlugIn(IKernel kernel, params TemplateExamples[]? examples)
    {
        if (kernel == null)
            throw new ArgumentNullException(nameof(kernel));

        var prompt = PromptBuilder.Build(examples);

        this.intentSkFunction = kernel.CreateSemanticFunction(prompt,
            description: "Determine the Intent and entities within the provide text.",
            maxTokens: PromptBuilder.MaxTokens,
            temperature: PromptBuilder.Temperature,
            topP: PromptBuilder.TopP);

    }

    [SKFunction, Description("Determine the Intent and entities within the provide text.")]
    public Task<SKContext> ExtractIntentAndEntity(
        [Description("determine the purpose and elements within the given text")] string input,
        SKContext context)
    {
        return this.intentSkFunction
            .AggregatePartitionedResultsAsync(new List<string>() { input }, context);
    }
}