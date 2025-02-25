using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace AzureAI.Community.SK.Plugin.MermaidPlugin
{
    public class MermaidPlugin
    {
        [KernelFunction, Description("This function helps create Mermaid code , based on your steps")]
        public async Task<string> GenerateMermaid(Kernel kernel,
            [Description("Describe the statements as steps to help create the Mermaid code.")]
            string statements)
        {
            if (string.IsNullOrEmpty(statements))
                return string.Empty;

            var prompt = GeneratePrompt(statements);

            var mermaidPromptResult = await kernel.InvokePromptAsync(prompt);

            var mermaidCode = mermaidPromptResult.GetValue<string>();

            if (string.IsNullOrEmpty(mermaidCode))
                return string.Empty;

            mermaidCode = Helper.GetMermaidCodeBlock(mermaidCode);

            return mermaidCode;
        }

        private static string GeneratePrompt(string statements)
        {
            var prompt =
                $"Can you provide kroki based mermaid code and select a diagram type based on the steps \n {statements} \n and provide only the mermaid code as the response, with no comments or mermaid code block delimiters.";

            var examples = Helper.GetDiagramString();

            prompt += $"\n\nExamples:\n{examples}";

            return prompt;
        }

    }
}
