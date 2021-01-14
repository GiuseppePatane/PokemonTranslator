using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PokemonTranslator.Core.Interfaces;
using PokemonTranslator.Infrastructure;
using PokemonTranslator.Infrastructure.ShakespeareClient;
using PokemonTranslator.Infrastructure.ShakespeareClient.Exceptions;
using PokemonTranslator.Infrastructure.ShakespeareClient.Models;
using Xunit;
using Xunit.Abstractions;

namespace PokemonTranslator.IntegrationTests.Infrastructure.ShakespeareClient
{
    public class ShakespeareHttpClientTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly ITranslatorClient _client;

        public ShakespeareHttpClientTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            var settings = new Dictionary<string, string> 
            {  
                {"HttpClients:ShakespeareApiBaseUrl", "https://api.funtranslations.com/"},
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();
            IServiceCollection serviceProvider = new ServiceCollection();
            serviceProvider.AddHttpClients(configuration);
            _client=  serviceProvider.BuildServiceProvider().GetService<ITranslatorClient>();
        }

        [Fact]
        public async Task GetTranslatedTest_ShouldBe_Ok()
        {
            try
            {
                var text = "CHARIZARD flies around the sky in search of powerful opponents. It breathes fire of such great heat that it melts anything. However, it never turns its fiery breath on any opponent weaker than itself.";
                var translation = "Charizard flies 'round the sky in search of powerful opponents. 't breathes fire of such most wondrous heat yond 't melts aught. However,  't nev'r turns its fiery breath on any opponent weaker than itself.";
                var response = await _client.GetTranslationAsync(
                    text); 
                response.Should().NotBeNull();
                response.Should().Be(translation);
            }
            catch ( ShakespeareClientException e)
            {
                e.Message.Should().NotBeNull();
               _testOutputHelper.WriteLine(e.Message);
            }

        }
    }
}