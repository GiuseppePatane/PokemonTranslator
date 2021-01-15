using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PokemonTranslator.Core.Interfaces;
using PokemonTranslator.Infrastructure.ShakespeareClient.Exceptions;
using PokemonTranslator.Infrastructure.ShakespeareClient.Models;

namespace PokemonTranslator.Infrastructure.ShakespeareClient
{
    public class ShakespeareHttpClient : ITranslatorClient
    {
        private readonly HttpClient _client;

        public ShakespeareHttpClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetTranslationAsync(string text)
        {
            using var response = await _client.GetAsync(UrlConfig.GetShakespeareTranslation(text));
            string content = null;
            try
            {
                content = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                var translationResponse = JsonSerializer.Deserialize<TranslationResponse>(content);
                return translationResponse?.Contents?.Translated;
            }
            catch (HttpRequestException ex) when (content != null)
            {
                var errorDetails = JsonSerializer.Deserialize<ErrorResponse>(content);
                throw new ShakespeareClientException(errorDetails?.Error?.Message);
            }
            catch (HttpRequestException ex) when (content == null)
            {
                throw new ShakespeareClientException(ex.Message);
            }
        }
    }
}