using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PokemonTranslator.Core.Interfaces;

namespace PokemonTranslator.Infrastructure.ShakespeareClient
{
    public class ShakespeareHttpClient : ITranslatorClient
    {
        private readonly HttpClient _client;

        public ShakespeareHttpClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetTranslation(string text)
        {
          var response  = await  _client.GetFromJsonAsync<TranslationResponse>(UrlConfig.GetShakespeareTranslation(text));
          
          
          return response.Contents.Translated;
        }
    }
}