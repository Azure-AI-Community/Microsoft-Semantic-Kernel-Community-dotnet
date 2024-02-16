# FlightTrackerPlugin

## Description

The FlightTrackerPlugin is a Semantic Kernel plugin that tracks the flight status of a provided source and destination. It uses the [Aviation Stack API](https://aviationstack.com/) to fetch flight details and status.

## Parameters

The TrackFlightAsync method takes the following parameters:

- `source`: IATA code for the source location.
- `destination`: IATA code for the destination location.
- `flightNumber`: IATA code for the flight.
- `limit`: Count of flights.

## Return Value

The TrackFlightAsync method returns a `JSON` tring containing the flight details and status.

## How to Use

To use this plugin, you need to create an instance of the FlightTrackerPlugin class with your Aviation Stack API key as a parameter.

```csharp

// Create a kernel with the Azure OpenAI chat completion service
var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion("model","endpoint","key");

// Load the plugins
builder.Plugins.AddFromObject(new FlightTrackerPlugin("your own API key"), nameof(FlightTrackerPlugin));

// Build the kernel
var kernel = builder.Build();

var funcResult = await kernel.InvokeAsync("FlightTrackerPlugin","TrackFlight", new KernelArguments() { ["source"] = "DXB", ["destination"] = "SYD", ["limit"] = limit } );

```
