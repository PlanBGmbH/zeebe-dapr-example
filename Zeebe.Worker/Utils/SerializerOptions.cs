using System.Text.Json;

namespace Zeebe.Worker.Utils
{
    public static class SerializerOptions
    {
        public static JsonSerializerOptions Json => new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            IgnoreNullValues = true,
        };
    }
}
