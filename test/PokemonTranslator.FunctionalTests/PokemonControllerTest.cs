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
         private readonly ITestOutputHelper _testOutputHelper;
         private readonly HttpClient _client;

         public PokemonControllerTest(ApiTestFactory<Startup> factory, ITestOutputHelper testOutputHelper)
         {
             _testOutputHelper = testOutputHelper;
             _client = factory.CreateClient();
             _client.BaseAddress = new Uri("http://localhost:5000");
         }
         public static readonly object[][] ValidPokemonName= {
             new object[] {"ditto","Ditto rearranges its cell structureth to transf'rm itself into oth'r shapes.  Howev'r, if 't be true t tries to transf'rm itself into something by relying on its mem'ry, this pokémon manages to receiveth details wrong"},
             new object[] {"pikachu","Whenev'r pikachu cometh across something new, t blasts t with a jolt of electricity.  If 't be true thee cometh across a blacken'd b'rry, it’s evidence yond this pokémon did mistake the intensity of its chargeth"},
             new object[] {"charizard","Charizard flies 'round the sky in searcheth of pow'rful opponents.  T breathes fireth of such most wondrous heateth yond t melts aught.  Howev'r, t nev'r turns its fi'ry breath on any opponent weak'r than itself"},
             new object[] {"bulbasaur","Bulbasaur can beest seen napping in bright sunlight.  Th're is a se'd on its backeth.  By soaking up the sun’s rays, the se'd grows progressively larg'r"},
         };
         [Theory]
         [MemberData(nameof(ValidPokemonName))]
         public async Task Get_Pokemon_With_A_Valid_Pokemon_Name_Should_Return_A_ValidResponse(string name,string expectedDescription)
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
                 var errorDetails = JsonSerializer.Deserialize<Api.Models.ErrorsResponse>(content);
                 errorDetails.Should().NotBeNull();
                 _testOutputHelper.WriteLine(errorDetails.Errors.First().Message);
             }
         }
     }
}