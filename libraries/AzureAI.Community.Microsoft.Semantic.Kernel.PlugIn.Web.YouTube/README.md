[Azure AI Community GitHub](https://github.com/Azure-AI-Community)

# AzureAI Community Microsoft Semantic Kernel Plugin - YouTube Videos Search

The `AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.YouTube` This component offers a streamlined solution for conducting YouTube video searches, allowing users to specify keywords and seamlessly integrate YouTube video data into their projects.


# Building a YouTube Video Connector

In this guide, we will walk you through the process of building a plugin to search for YouTube videos and retrieve video details . 

## Prerequisites

Install AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.YouTube package from nuget server

Before you begin building the plugin, make sure you have the following prerequisites in place:

**YouTube Data API Key**: You'll need an API key from the YouTube Data API to access YouTube's data. You can obtain one by following the API key setup instructions on the [YouTube API Documentation](https://developers.google.com/youtube/registering_an_application).

## Plugin Building Steps

### Create an Instance of YouTubeConnector

The `YouTubeConnector` class is responsible for conducting the search for YouTube videos. You need to create an instance of this class by providing your YouTube API key

Here's an example code snippet:

```csharp

 var youTubeConnector = new YouTubeConnector("YouTube-Key");

```

### Importing a Plugin into the Kernel

To import a Plugin into the kernel, you can use the following C# code snippet:

```csharp
  var youtubePlugin = kernel.ImportFunctions(new WebSearchEnginePlugin(youTubeConnector), nameof(YouTubeConnector));

```

### Running a Plugin within the Kernel

To execute a Plugin within the kernel, you can use the following C# code snippet:

```csharp
var result = await kernel.RunAsync("Bot composer", youtubePlugin["search"]);
```

### Output
```csharp
Console.WriteLine(result);

```

## Channels based search
YouTubeConnector offers robust support for conducting searches based on YouTube channels. 
This functionality allows users to efficiently retrieve and analyze content associated with specific YouTube channels.

When initializing the YouTubeConnector, simply supply the channel ID.

Here's an example code snippet:

```csharp

 var youTubeConnector = new YouTubeConnector("YouTube-Key","youTube-ChannelId");

```