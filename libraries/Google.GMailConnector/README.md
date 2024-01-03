[Azure AI Community GitHub](https://github.com/Azure-AI-Community)

# AzureAI Community Microsoft Semantic Kernel Plugin - Google GMailConnector

The ` AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Google.GMailConnector` This component offers send email using GMail 


# Building a Google GMail Connector

This guide will lead you through the steps of creating a plugin for utilizing Gmail services to send and retrieve emails.

## Prerequisites

Install AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Google.GMailConnector package from nuget server


## Plugin Building Steps

### Create an Instance of GMailConnector

The `GMailConnector` class is responsible for sending emails. You need to provide an instance of the GmailService class to this class.

Here's an example code snippet:

```csharp
	
	GmailService gmailService;
	IEmailConnector gMailConnector = new GMailConnector(gmailService);

```

### Importing a Plugin into the Kernel

To import a Plugin into the kernel, you can use the following C# code snippet:

```csharp
	EmailPlugin emailPlugin = new EmailPlugin(gMailConnector);
	var gmail = kernel.ImportFunctions(emailPlugin, nameof(EmailPlugin));

```

### Running a Plugin within the Kernel

To run a plugin within the kernel and dispatch an email., you can use the following C# code snippet:

```csharp
var result = await kernel.RunAsync("Bot composer",  gmail["SendEmail"]);
```

### Output
```csharp
Console.WriteLine(result);

```

## Get my email address
```csharp

  SKContext emailAddressResult = await kernel.RunAsync(string.Empty, gmail["GetMyEmailAddress"]);
  string myEmailAddress = emailAddressResult.Result;

```

## GetMessagesAsync
```csharp

  SKContext emailAddressResult = await kernel.RunAsync(string.Empty, gmail["GetMessagesAsync"]);
  var messages = emailAddressResult.Result;

```