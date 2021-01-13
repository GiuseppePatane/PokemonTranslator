using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PokemonTranslator.Core.Exceptions;
using PokemonTranslator.Core.Interfaces;
using PokemonTranslator.Infrastructure;
using Xunit;

namespace PokemonTranslator.IntegrationTests.Infrastructure.PokeApi
{
    public class PokemonClientIntegrationTest
    {
        private readonly IPokemonClient _pokemonClient;
        public PokemonClientIntegrationTest()
        {
            IServiceCollection serviceProvider = new ServiceCollection();
            serviceProvider.AddClients();
            _pokemonClient=  serviceProvider.BuildServiceProvider().GetService<IPokemonClient>();
        }
        public static readonly object[][] ValidPokemonName= {
            new object[] {"ditto"},
            new object[] {"pikachu"},
            new object[] {"charizard"},
            new object[] {"bulbasaur"},
            new object[] {"DITTO"}
        };
        public static readonly object[][] InValidPokemonName= {
            new object[] {"pikkachu"},
            new object[] {string.Empty},
            new object[] {"    "},
            new object[] {"chazzrizard"},
            new object[] {"bulaassbasaur"},
            new object[] {"Goku"},
            new object[] {"Gennaro Savastano"}
        };
        [Theory]
        [MemberData(nameof(ValidPokemonName))]
        public async Task GetPokemonRaceAsync_With_A_Valid_PokemonName_Should_Return_A_PokemonRace(string name)
        {
          var pokemonRace  = await  _pokemonClient.GetPokemonRaceAsync(name);
          pokemonRace.Should().NotBeNull();
          pokemonRace.Id.Should().BeGreaterThan(0);
          pokemonRace.Name.Should().Be(name.ToLower());
          pokemonRace.Description.Should().NotBeNullOrEmpty();
        }
        
        [Theory]
        [MemberData(nameof(InValidPokemonName))]
        public async Task GetPokemonRaceAsync_With_A_Invalid_PokemonName_Should_Return_An_PokemonNotFoundException(string name)
        {
            await Assert.ThrowsAnyAsync<PokemonNotFoundException>(async () =>
            {
                await _pokemonClient.GetPokemonRaceAsync(name);
            });
        }
    }
    
   
}