[Azure AI Community GitHub](https://github.com/Azure-AI-Community)

## Intent Detection and Entity Recognition - AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Intent

The `AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Intent` plug-in is a powerful tool used to identify and recognize intents and entities within a given text.

### Intent Detection

The plug-in allows you to determine the primary intention behind a piece of text. This is valuable in natural language understanding applications, such as chatbots or virtual assistants, where understanding user intentions is crucial.

### Entity Recognition

In addition to intent detection, this plug-in excels at entity recognition. It can identify specific entities within the text, providing valuable information that can be used to extract meaningful data.

Whether you're building chatbots, conducting sentiment analysis, or performing any other text-based analysis, the `AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Intent` plug-in is a valuable asset for enhancing the understanding of textual data.



## Example: Using AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Intent Plug-in

In this example, we demonstrate how to use the `AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Intent` plug-in for intent detection and entity recognition.

```csharp
IDictionary<string, ISKFunction> conversationSummarySkill =
    kernel.ImportSkill(new IntentPlugIn(kernel));

SKContext intentInformation = await kernel.RunAsync("how is weather in chennai at 4PM",
    conversationSummarySkill["ExtractIntentAndEntity"]);

```
output:

```json
{
  "text": "how is weather in chennai at 4PM",
  "intent": {
    "name": "Weather Inquiry",
    "score": 0.90
  },
  "entities": [
    {
      "entity": "Location",
      "type": "Location",
      "startPos": 15,
      "endPos": 22,
      "value": "Chennai"
    },
    {
      "entity": "Time",
      "type": "Time",
      "startPos": 25,
      "endPos": 29,
      "value": "4PM"
    }
  ]
}

```

## Customizable Template Examples with the AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Intent Plug-in

With the `AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Intent` plug-in, developers have the flexibility to provide their own 'N' of examples based on their specific needs.(The default template has been replaced.)

### Code Example

You can pass custom examples to the plug-in as follows:

```csharp
var template1 = new TemplateExamples
{
    Input = "I am planning a software development project for our new mobile app, scheduled to start next month.",
    Output = @"{
      ""text"": ""I am starting a new software development project for our mobile app next month"",
      ""intent"": {
        ""name"": ""Project Planning"",
        ""score"": 0.85
      },
      ""entities"": [
        {
          ""entity"": ""ProjectType"",
          ""type"": ""Development Project"",
          ""startPos"": 18,
          ""endPos"": 44,
          ""value"": ""software development project""
        },
        {
          ""entity"": ""ProjectName"",
          ""type"": ""Project Name"",
          ""startPos"": 49,
          ""endPos"": 61,
          ""value"": ""mobile app""
        },
        {
          ""entity"": ""StartDate"",
          ""type"": ""Start Date"",
          ""startPos"": 73,
          ""endPos"": 83,
          ""value"": ""Augest""
        }
      ]
    }"
};
```

## Constructing the Object with Template Information

you need to provide the template information while constructing the object. Here's an example of how to do it:

### Example Code

```csharp
IDictionary<string, ISKFunction> conversationSummarySkill =
    kernel.ImportSkill(new IntentPlugIn(kernel, template1));

