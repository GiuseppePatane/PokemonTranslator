using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PokemonTranslator.Api.Models
{
    public class Error
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    public class ErrorsResponse
    {
        public ErrorsResponse()
        {
            Errors = new List<Error>();
        }
        [JsonPropertyName("errors")]
        public List<Error> Errors { get; set; }
    }
}