using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyBudget.Api.Tests;
internal static class Constants
{
    public static JsonSerializerOptions DefaultJsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };
}
