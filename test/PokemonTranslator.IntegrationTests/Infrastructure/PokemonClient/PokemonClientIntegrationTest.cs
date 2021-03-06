using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PokemonTranslator.Core.Exceptions;
using PokemonTranslator.Core.Interfaces;
using PokemonTranslator.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace PokemonTranslator.IntegrationTests.Infrastructure.PokemonClient
{
    public class PokemonClientIntegrationTest
    {
        public static readonly object[][] ValidPokemonNames =
        {
            new object[]
            {
                "ditto",
                "DITTO rearranges its cell structure to transform itself into other shapes. However, if it tries to transform itself into something by relying on its memory, this POKéMON manages to get details wrong."
            },
            new object[]
            {
                "charizard",
                "CHARIZARD flies around the sky in search of powerful opponents. It breathes fire of such great heat that it melts anything. However, it never turns its fiery breath on any opponent weaker than itself."
            },
            new object[]
            {
                "pikachu",
                "Pikachu that can generate powerful electricity have cheek sacs that are extra soft and super stretchy."
            },
            new object[]
            {
                "bulbasaur",
                "BULBASAUR can be seen napping in bright sunlight. There is a seed on its back. By soaking up the sun’s rays, the seed grows progressively larger."
            },
            new object[]
            {
                "DITTO",
                "DITTO rearranges its cell structure to transform itself into other shapes. However, if it tries to transform itself into something by relying on its memory, this POKéMON manages to get details wrong."
            }
        };

        public static readonly object[][] InValidPokemonNames =
        {
            new object[] {"pikkachu"},
            new object[] {string.Empty},
            new object[] {"    "},
            new object[] {"chazzrizard"},
            new object[] {"bulaassbasaur"},
            new object[] {"Goku"},
            new object[] {"Gennaro Savastano"}
        };

        private readonly IPokemonClient _pokemonClient;
        private readonly ITestOutputHelper _testOutputHelper;

        public PokemonClientIntegrationTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            IServiceCollection serviceProvider = new ServiceCollection();
            serviceProvider.AddClients();
            _pokemonClient = serviceProvider.BuildServiceProvider().GetService<IPokemonClient>();
        }

        [Theory]
        [MemberData(nameof(ValidPokemonNames))]
        public async Task GetPokemonRaceAsync_With_A_Valid_PokemonName_Should_Return_A_PokemonRace(string name,
            string expectedDescription)
        {
            var pokemonRace = await _pokemonClient.GetPokemonRaceAsync(name);
            pokemonRace.Should().NotBeNull();
            pokemonRace.Id.Should().BeGreaterThan(0);
            pokemonRace.Name.Should().Be(name.ToLower());
            pokemonRace.Description.Should().Be(expectedDescription);
            _testOutputHelper.WriteLine(pokemonRace.Description);
        }

        [Theory]
        [MemberData(nameof(InValidPokemonNames))]
        public async Task GetPokemonRaceAsync_With_A_Invalid_PokemonName_Should_Return_An_PokemonNotFoundException(
            string name)
        {
            await Assert.ThrowsAnyAsync<PokemonNotFoundException>(async () =>
            {
                await _pokemonClient.GetPokemonRaceAsync(name);
            });
        }
    }
}