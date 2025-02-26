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
