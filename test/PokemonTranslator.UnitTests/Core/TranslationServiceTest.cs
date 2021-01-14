using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using PokemonTranslator.Core.Domain;
using PokemonTranslator.Core.Interfaces;
using PokemonTranslator.Core.Services;
using Xunit;

namespace PokemonTranslator.UnitTests.Core
{
    public class TranslationServiceTest
    {
        private readonly Mock<IPokemonClient> _pokemonClientMock;
        private readonly Mock<ITranslatorClient> _translatorClient;
        public TranslationServiceTest()
        {
            _pokemonClientMock = new Mock<IPokemonClient>();
            _translatorClient = new Mock<ITranslatorClient>();
        }

        [Fact]
        public async Task GetPokemonTranslationAsync_With_A_Valid_Pokemon_Name_Should_Return_A_PokemonTranslationReadModel()
        {
            var pokemonId = 1;
            var pokemonName = "pippo";
            var description = "pippo pokemon";
            _pokemonClientMock
                .Setup(x => x.GetPokemonRaceAsync(pokemonName))
                .ReturnsAsync(PokemonRace.Create(pokemonId, pokemonName, description));
            var service = new TranslatorService(_pokemonClientMock.Object,_translatorClient.Object);
            var response = await service.GetPokemonTranslationAsync(pokemonName);
            response.Should().NotBeNull();
            response.Name.Should().Be(pokemonName);
            response.Description.Should().Be(description);
            response.Translation.Should().BeNullOrEmpty();


        }
    }
}