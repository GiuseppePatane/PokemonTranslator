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
             new object[] {"ditto","DITTO rearranges its cell structure to transform itself into other shapes. However, if it tries to transform itself into something by relying on its memory, this POKéMON manages to get details wrong."},
             new object[] {"pikachu","Whenever PIKACHU comes across something new, it blasts it with a jolt of electricity. If you come across a blackened berry, it’s evidence that this POKéMON mistook the intensity of its charge."},
             new object[] {"charizard","CHARIZARD flies around the sky in search of powerful opponents. It breathes fire of such great heat that it melts anything. However, it never turns its fiery breath on any opponent weaker than itself."},
             new object[] {"bulbasaur","BULBASAUR can be seen napping in bright sunlight. There is a seed on its back. By soaking up the sun’s rays, the seed grows progressively larger."},
             new object[] {"DITTO","DITTO rearranges its cell structure to transform itself into other shapes. However, if it tries to transform itself into something by relying on its memory, this POKéMON manages to get details wrong."}
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