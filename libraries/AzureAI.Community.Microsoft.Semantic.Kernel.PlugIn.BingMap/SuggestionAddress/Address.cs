using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Web.BingMap.SuggestionAddress;

public class Address
{
    public string Name { get; set; }
    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public string Location { get; set; } = string.Empty;

    public string Serialize(bool includeCoordinates)
    {
        var address = includeCoordinates ? JsonConvert.SerializeObject(this, Formatting.Indented) :
            JsonConvert.SerializeObject(Location, Formatting.Indented);

        address = Regex.Unescape(address);

        return address;
    }

    public static Address? Deserialize(string jsonString)
    {
        return JsonConvert.DeserializeObject<Address>(jsonString);
    }
}