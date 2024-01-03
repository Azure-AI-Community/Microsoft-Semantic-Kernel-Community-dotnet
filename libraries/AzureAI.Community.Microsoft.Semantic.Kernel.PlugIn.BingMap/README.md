# Microsoft-Semantic-Kernel-Community-dotnet

You can find the latest release notes and updates for the Microsoft Semantic Kernel Community .NET at the following link:
[Release Notes](https://github.com/Azure-AI-Community/Microsoft-Semantic-Kernel-Community-dotnet)

# SuggestionAddressPlugin

The `AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.BingMap` This component offers a streamlined solution for conducting Bing maps location searches.


# Building a SuggestionAddressPlugin

We'll take you through the steps of constructing a plugin for location-based search suggestions.

## Prerequisites

Install AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.BingMap package from nuget server

Before you begin building the plugin, make sure you have the following prerequisites in place:

**Azure Bing Maps API Key**: Obtain an API key from Azure Bing Maps to gain access to location data.

`SuggestionAddressPlugin` is a class designed for discovering addresses based on suggestions such as hotels, coffee shops, restaurants, and petrol stations.

### Create an Instance of SuggestionAddressPlugin

The `SuggestionAddressPlugin` class is responsible for conducting the search for location-based search suggestions . You need to create an instance of this class by providing your YouTube API key

Here's an example code snippet:

```csharp

 var suggestionAddressPlugin = new SuggestionAddressPlugin("Bing Map-Key");

```

## Method Signature

```csharp
public async Task<string> SearchSuggestionAsync([Description("suggestion")] string suggestion, [Description("city name")] string city)
```

## Parameters
suggestion: A string parameter that is meant to receive input related to address suggestions.

city: A string parameter that is intended for specifying the city associated with the search.

## Return Value
The method returns a Task<string>, indicating that the operation is asynchronous and will result in a string being returned. The returned string is expected to represent a JSON collection containing address-related information. Within this JSON collection, you can find details such as Longitude, Latitude, and Location information associated with the suggested addresses

## Example Usage

```csharp

//Add the location search plugin 
var location = new SuggestionAddressPlugin("Bing map key");
builder.Plugins.AddFromObject(location);

//Create kernel function
var suggestionKernelFunction =
    kernel.Plugins.GetFunction(nameof(SuggestionAddressPlugin), "SearchSuggestion");

//Trigger invoke function
var result1 = await kernel.InvokeAsync(suggestionKernelFunction, new KernelArguments
{
    { "suggestion", "restaurants" },
    { "city", "Thanjavur" }
});

Console.WriteLine(result1.GetValue<string>());

```

### Output

# JSON Response

```json
[
  {
    "Longitude": 79.134483,
    "Latitude": 10.784559,
    "Location": "Big Temple Road, Near Brihadeeeswara Temple, Pudupattinam Thanjavur, Thanjavur Railway Station Area, Thanjavur 613001, Tamil Nadu"
  },
  {
    "Longitude": 79.134005,
    "Latitude": 10.784972,
    "Location": "Thiruvalluvar Salai, Thanjavur Railway Station Area, Thanjavur 613001, Tamil Nadu"
  },
  {
    "Longitude": 79.133303,
    "Latitude": 10.781487,
    "Location": "Big Temple Road, Near Swetha Complex, Membalam Thanjavur, Membalam, Thanjavur 613007, Tamil Nadu"
  },
  {
    "Longitude": 79.133807,
    "Latitude": 10.786525,
    "Location": "Thanjavur Palace Area, Thanjavur 613001, Tamil Nadu"
  },
  {
    "Longitude": 79.133546,
    "Latitude": 10.78647,
    "Location": "South Rampart Road, Thanjavur Palace Area, Thanjavur 613001, Tamil Nadu"
  },
  {
    "Longitude": 79.133366,
    "Latitude": 10.780889,
    "Location": "Big Temple Road, Near Taluka Office, Pudupattinam Thanjavur, Thanjavur Railway Station Area, Thanjavur 613001, Tamil Nadu"
  },
  {
    "Longitude": 79.134151,
    "Latitude": 10.786354,
    "Location": "S Rampart Road, Thanjavur Palace Area, Thanjavur 613001, Tamil Nadu"
  },
  {
    "Longitude": 79.132854,
    "Latitude": 10.780708,
    "Location": "Big Temple Road, Near Taluka Office, Pudupattinam Thanjavur, Thanjavur Railway Station Area, Thanjavur 613001, Tamil Nadu"
  },
  {
    "Longitude": 79.133112,
    "Latitude": 10.780386,
    "Location": "Medical College Road, South Rampart, Thanjavur Railway Station Area, Thanjavur 613001, Tamil Nadu"
  },
  {
    "Longitude": 79.134941,
    "Latitude": 10.786219,
    "Location": "Thiruvalluvar Theatre Lane, Near Hotel Pla Inn, Thanjavur Palace Area, Thanjavur 613001, Tamil Nadu"
  }
]
