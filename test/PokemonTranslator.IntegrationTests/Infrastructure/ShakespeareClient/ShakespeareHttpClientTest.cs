using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokemonTranslator.Core.Interfaces;
using PokemonTranslator.Infrastructure;
using PokemonTranslator.Infrastructure.ShakespeareClient;
using Xunit;

namespace PokemonTranslator.IntegrationTests.Infrastructure.ShakespeareClient
{
    public class ShakespeareHttpClientTest
    {
        private readonly ShakespeareHttpClient _client;

        public ShakespeareHttpClientTest()
        {
            var settings = new Dictionary<string, string> 
            {  
                {"HttpClients:ShakespeareApiBaseUrl", "https://api.funtranslations.com/"},
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();
            IServiceCollection serviceProvider = new ServiceCollection();
            serviceProvider.AddHttpClients(configuration);
            _client=  serviceProvider.BuildServiceProvider().GetService<ShakespeareHttpClient>();
        }

        [Fact]
        public async Task GetTranslatedTest_ShouldBe_Ok()
        {
            var text =
                " A strange seed was planted on its back at birth. The plant sprouts and grows with this POKéMON.A strange seed was planted on its back at birth.";
           var response = await _client.GetTranslation(
                text);

           response.Should().NotBeNull();
           
        }
    }
}