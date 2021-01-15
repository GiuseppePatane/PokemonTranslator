using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using PokemonTranslator.Core.Domain;
using PokemonTranslator.Core.Exceptions;
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
        public async Task
            GetPokemonTranslationAsync_With_A_Invalid_Pokemon_Name_Should_Return_A_PokemonNotFoundException()
        {
            var pokemonName = "Alfio";
            _pokemonClientMock
                .Setup(x => x.GetPokemonRaceAsync(pokemonName)).ThrowsAsync(new PokemonNotFoundException());
            var service = new TranslatorService(_pokemonClientMock.Object, _translatorClient.Object);
            await Assert.ThrowsAnyAsync<PokemonNotFoundException>(async () =>
            {
                await service.GetPokemonTranslationAsync(pokemonName);
            });
        }

        [Fact]
        public async Task
            GetPokemonTranslationAsync_With_A_Valid_Pokemon_Name_Should_Return_A_PokemonTranslationReadModel()
        {
            var pokemonId = 1;
            var pokemonName = "pikachu";
            var description = "Whenever PIKACHU comes across something new, it blasts it with a jolt of electricity.";
            var translation = "Whenev'r pikachu cometh across something new, t blasts t with a jolt of electricity";
            _pokemonClientMock
                .Setup(x => x.GetPokemonRaceAsync(pokemonName))
                .ReturnsAsync(PokemonRace.Create(pokemonId, pokemonName, description));
            _translatorClient.Setup(x => x.GetTranslationAsync(description)).ReturnsAsync(translation);
            var service = new TranslatorService(_pokemonClientMock.Object, _translatorClient.Object);
            var response = await service.GetPokemonTranslationAsync(pokemonName);
            response.Should().NotBeNull();
            response.Name.Should().Be(pokemonName);
            response.Description.Should().Be(description);
            response.Translation.Should().Be(translation);
        }
    }
}