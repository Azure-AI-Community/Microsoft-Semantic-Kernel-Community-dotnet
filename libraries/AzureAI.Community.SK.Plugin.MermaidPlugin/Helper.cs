using System.Text.RegularExpressions;
using System.Text;

namespace AzureAI.Community.SK.Plugin.MermaidPlugin;

static class Helper
{
    public static string GenerateUniqueFilename(string fileExtension)
    {
        // Get current timestamp in milliseconds
        long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        // Generate a unique identifier (GUID)
        string uniqueId = Guid.NewGuid().ToString("N");

        // Combine timestamp and unique identifier to create a unique filename
        return $"diagram_{timestamp}_{uniqueId}{fileExtension}";
    }

    public static string GetMermaidCodeBlock(string input)
    {
        var pattern = @"```mermaid(.*?)```";
        var match = Regex.Match(input, pattern, RegexOptions.Singleline);
        return match.Success ? match.Groups[1].Value.Trim() : input;
    }

    public static string GetDiagramString()
    {
        StringBuilder sb = new StringBuilder();

        // Adding Sequence Diagram
        sb.AppendLine();
        sb.AppendLine("seqdiag {");
        sb.AppendLine("  browser  -> webserver [label = \"GET /index.html\"];");
        sb.AppendLine("  browser <-- webserver;");
        sb.AppendLine("  browser  -> webserver [label = \"POST /blog/comment\"];");
        sb.AppendLine("  webserver  -> database [label = \"INSERT comment\"];");
        sb.AppendLine("  webserver <-- database;");
        sb.AppendLine("  browser <-- webserver;");
        sb.AppendLine("}");
        sb.AppendLine();

        // Adding Block Diagram
        sb.AppendLine();
        sb.AppendLine("blockdiag {");
        sb.AppendLine("  blockdiag -> generates -> \"block-diagrams\";");
        sb.AppendLine("  blockdiag -> is -> \"very easy!\";");
        sb.AppendLine();
        sb.AppendLine("  blockdiag [color = \"greenyellow\"];");
        sb.AppendLine("  \"block-diagrams\" [color = \"pink\"];");
        sb.AppendLine("  \"very easy!\" [color = \"orange\"];");
        sb.AppendLine("}");
        sb.AppendLine();

        // Adding UML Diagram
        sb.AppendLine();
        sb.AppendLine("[Pirate|eyeCount: Int|raid();pillage()|");
        sb.AppendLine("  [beard]--[parrot]");
        sb.AppendLine("  [beard]-:>[foul mouth]");
        sb.AppendLine("]");
        sb.AppendLine();
        sb.AppendLine("[<abstract>Marauder]<:--[Pirate]");
        sb.AppendLine("[Pirate]- 0..7[mischief]");
        sb.AppendLine("[jollyness]->[Pirate]");
        sb.AppendLine("[jollyness]->[rum]");
        sb.AppendLine("[jollyness]->[singing]");
        sb.AppendLine("[Pirate]-> *[rum|tastiness: Int|swig()]");
        sb.AppendLine("[Pirate]->[singing]");
        sb.AppendLine("[singing]<->[rum]");
        sb.AppendLine();
        sb.AppendLine("[<start>st]->[<state>plunder]");
        sb.AppendLine("[plunder]->[<choice>more loot]");
        sb.AppendLine("[more loot]->[st]");
        sb.AppendLine("[more loot] no ->[<end>e]");
        sb.AppendLine();
        sb.AppendLine("[<actor>Sailor] - [<usecase>shiver me;timbers]");

        return sb.ToString();
    }
}