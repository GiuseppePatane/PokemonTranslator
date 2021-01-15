using System.Text.Json.Serialization;

namespace PokemonTranslator.Infrastructure.ShakespeareClient.Models
{
    public class Error
    {
        [JsonPropertyName("code")] public int Code { get; set; }

        [JsonPropertyName("message")] public string Message { get; set; }
    }

    public class ErrorResponse
    {
        [JsonPropertyName("error")] public Error Error { get; set; }
    }
}