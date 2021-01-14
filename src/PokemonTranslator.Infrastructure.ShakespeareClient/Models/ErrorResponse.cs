using System.Text.Json.Serialization;

namespace PokemonTranslator.Infrastructure.ShakespeareClient.Models
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Error    {
        [JsonPropertyName("code")]
        public int Code; 

        [JsonPropertyName("message")]
        public string Message; 
    }

    public class Root    {
        [JsonPropertyName("error")]
        public Error Error; 
    }
}