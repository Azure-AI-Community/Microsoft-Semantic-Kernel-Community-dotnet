[Azure AI Community GitHub](https://github.com/Azure-AI-Community)

# AzureAI Community Semantic Kernel Plugin - Visualization Plugin

The `AzureAI.Community.SK.Plugin.VisualizationPlugin` is a plugin designed to convert Mermaid diagrams into images. This plugin provides the `VisualizationToImage` function, which accepts Mermaid code and generates a visual representation of the diagram as an image.

## Overview

The `VisualizationPlugin` takes Mermaid code as input and converts it into an image, making it easy to visualize diagrams directly from text descriptions. By integrating this plugin with the Azure AI Semantic Kernel, you can automate the diagram creation and visualization process.

## Prerequisites

To use the `VisualizationPlugin`, ensure the following:

- Azure AI Semantic Kernel is installed.
- Access to the Semantic Kernel Plugin system.

## Installing the Plugin

To install the `VisualizationPlugin`, add it to your project by importing it into the kernel.

### Example Code:

```csharp
// Add the VisualizationPlugin to the Kernel
kernel.Plugins.AddFromType<VisualizationPlugin>(nameof(VisualizationPlugin));

```

## Example Usage

string diagramDescription = @"
The decision starts with a question: Is it sunny? If the answer is yes, you choose to go for a walk and enjoy it.
If the answer is no, you decide to stay indoors and read a book. Both activities lead to the same conclusion: the end of the day. 
";

## Genreate image from the description using Mermaid code
![diagram_1740906220394_423672cf811c4f8188618ad007f66878](https://github.com/user-attachments/assets/a623dbd5-cfc9-4b46-9872-1909c089d8ce)


