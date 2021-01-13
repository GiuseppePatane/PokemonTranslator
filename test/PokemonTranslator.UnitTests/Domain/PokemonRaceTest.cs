using FluentAssertions;
using PokemonTranslator.Core.Domain;
using Xunit;

namespace PokemonTranslator.UnitTests.Domain
{
    public class PokemonRaceTest
    {
        [Fact]
        public void Create_New_PokemonRace_Should_Be_OK()
        {
            var pokemonId = 1;
            var pokemonName = "pippo";
            var description = "pippo pokemon";
            var pokemonRace = PokemonRace.Create(pokemonId, pokemonName, description);
            pokemonRace.Should().NotBeNull();
            pokemonRace.Id.Should().Be(pokemonId);
            pokemonRace.Name.Should().Be(pokemonName);
            pokemonRace.Description.Should().Be(description);
        }
        
    }
}