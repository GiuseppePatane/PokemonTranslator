using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using PokemonTranslator.Core.Exceptions;
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
            using (var response = await _client.GetAsync(UrlConfig.GetShakespeareTranslation(text)))
            {
                HttpContent content = null;
                try
                {
                    content = response.Content;
                    response.EnsureSuccessStatusCode();
                    var translationResponse = await content.ReadFromJsonAsync<TranslationResponse>();
                    return translationResponse?.Contents?.Translated;
                }
                catch (HttpRequestException ex) when (content != null)
                {
                    var errorDetails = await content.ReadFromJsonAsync<Root>();
                    throw new TooManyRequestException(errorDetails?.Error?.Message);
                }
            }
            // try
            // {
            //     var response = await _client.GetAsync(UrlConfig.GetShakespeareTranslation(text));
            //     response.StatusCode.is;
            //     var translationResponse = await response.Content.ReadFromJsonAsync<TranslationResponse>();
            //     return translationResponse?.Contents?.Translated;
            // }
            // catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.TooManyRequests)
            // {
            //     var dio = e.Message;
            //     throw;
            // }
        }
    }
}