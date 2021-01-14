using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using PokemonTranslator.Api;
using PokemonTranslator.Api.Controllers;
using Xunit;

namespace PokemonTranslator.FunctionalTests
{
    public class PokemonControllerTest : IClassFixture<ApiTestFactory<Startup>>
     {
         private readonly HttpClient _client;

         public PokemonControllerTest(ApiTestFactory<Startup> factory)
         {
             _client = factory.CreateClient();
             _client.BaseAddress = new Uri("http://localhost:5000");
         }

         [Fact]
         public async Task Get_Pokemon_With_A_Valid_Pokemon_Name_Should_Return_A_Not_Empty_PokemonDescriptionResponse()
         {
             var response = await _client.GetAsync("pokemon/ditto");
             response.EnsureSuccessStatusCode();
             var result = await  response.Content.ReadFromJsonAsync<PokemonDescriptionResponse>();
             result.Should().NotBeNull();
             result.Name.Should().Be("ditto");
             result.Description.Should().NotBeNullOrEmpty();
         }
     }
}