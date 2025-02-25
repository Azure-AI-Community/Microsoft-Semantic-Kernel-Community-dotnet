using System.ComponentModel;
using System.Text;
using Microsoft.SemanticKernel;

namespace AzureAI.Community.SK.Plugin.VisualizationPlugin
{
    public class VisualizationPlugin
    {
        /// <summary>
        /// Creates a diagram based on the provided Mermaid code using the Kroki.
        /// </summary>
        /// <param name="mermaidCode">The Mermaid code representing the diagram.</param>
        /// <returns>The file path of the saved diagram image.</returns>
        [KernelFunction, Description("Creates a diagram based on the provided Mermaid code")]
        public async Task<string> VisualizationToImage(string mermaidCode)
        {
            if (string.IsNullOrEmpty(mermaidCode))
                return string.Empty;

            var url = "https://kroki.io/mermaid/png";

            var payload = new
            {
                diagram_source = mermaidCode
            };

            // Serialize the payload to JSON
            string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

            // Initialize HttpClient
            using HttpClient client = new HttpClient();

            // Create the HttpContent object
            HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                // Send POST request to the Kroki
                HttpResponseMessage response = await client.PostAsync(url, content);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read the response content (image bytes)
                byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

                // Define the output file path
                string outputPath = GenerateUniqueFilename(".png");

                // Write the image bytes to a file
                await File.WriteAllBytesAsync(outputPath, imageBytes);

                // Output the result
                Console.WriteLine($"Diagram saved to {outputPath}");

                return outputPath;
            }

            catch (HttpRequestException ex)
            {
                // Handle the exception caused by the failed POST request
                // Log the error, display an error message, or take appropriate action
                Console.WriteLine($"Failed to create diagram: {ex.Message}");
            }

            return string.Empty;
        }

        public static string GenerateUniqueFilename(string fileExtension)
        {
            // Get current timestamp in milliseconds
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            // Generate a unique identifier (GUID)
            string uniqueId = Guid.NewGuid().ToString("N");

            // Combine timestamp and unique identifier to create a unique filename
            return $"diagram_{timestamp}_{uniqueId}{fileExtension}";
        }
    }
}
