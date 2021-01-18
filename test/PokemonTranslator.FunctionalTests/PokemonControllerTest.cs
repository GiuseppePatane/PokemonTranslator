using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using PokemonTranslator.Api;
using PokemonTranslator.Api.Models;
using Xunit;
using Xunit.Abstractions;

namespace PokemonTranslator.FunctionalTests
{
    public class PokemonControllerTest : IClassFixture<ApiTestFactory<Startup>>
    {
        public static readonly object[][] ValidPokemonName =
        {
            
            new object[]
            {
                "pikachu",
                "Whenev'r pikachu cometh across something new, t blasts t with a jolt of electricity. If 't be true thee cometh across a blacken'd b'rry, it’s evidence yond this pokémon did mistake the intensity of its chargeth"
            },
            new object[]
            {
                "charizard",
                "Charizard flies 'round the sky in search of powerful opponents. 't breathes fire of such most wondrous heat yond 't melts aught. However,'t nev'r turns its fiery breath on any opponent weaker than itself."
            },
        };

        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;

        public PokemonControllerTest(ApiTestFactory<Startup> factory, ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("http://localhost:5000");
        }

        [Theory]
        [MemberData(nameof(ValidPokemonName))]
        public async Task Get_Pokemon_With_A_Valid_Pokemon_Name_Should_Return_A_ValidResponse(string name,
            string expectedDescription)
        {
            using var response = await _client.GetAsync($"pokemon/{name}");
            string content = null;
            try
            {
                content = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                var pokemonDescriptionResponse = JsonSerializer.Deserialize<PokemonDescriptionResponse>(content);
                pokemonDescriptionResponse.Should().NotBeNull();
                pokemonDescriptionResponse.Description.Should().Be(expectedDescription);
                _testOutputHelper.WriteLine(pokemonDescriptionResponse.Description);
            }
            catch (HttpRequestException ex) when (content != null)
            {
                var errorDetails = JsonSerializer.Deserialize<ErrorsResponse>(content);
                errorDetails.Should().NotBeNull();
                _testOutputHelper.WriteLine(errorDetails.Errors.First().Message);
                _testOutputHelper.WriteLine(ex.Message);
            }
        }
    }
}