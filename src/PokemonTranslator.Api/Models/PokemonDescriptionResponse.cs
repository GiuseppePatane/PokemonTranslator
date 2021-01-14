using System.Text.Json.Serialization;

namespace PokemonTranslator.Api.Models
{
    public class PokemonDescriptionResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}