using System.Text.Json.Serialization;

namespace PokemonTranslator.Infrastructure.ShakespeareClient.Models
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Error    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    public class ErrorResponse    {
        [JsonPropertyName("error")]
        public Error Error { get; set; }
    }
}