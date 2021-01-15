using System.Threading.Tasks;
using PokemonTranslator.Core.Interfaces;
using PokemonTranslator.Core.ReadModels;

namespace PokemonTranslator.Core.Services
{
    public class TranslatorService : ITranslatorService
    {
        private readonly IPokemonClient _pokemonClient;
        private readonly ITranslatorClient _translatorClient;

        public TranslatorService(IPokemonClient pokemonClient, ITranslatorClient translatorClient)
        {
            _pokemonClient = pokemonClient;
            _translatorClient = translatorClient;
        }

        public async Task<PokemonTranslationReadModel> GetPokemonTranslationAsync(string pokemonName)
        {
            var pokemonRace = await _pokemonClient.GetPokemonRaceAsync(pokemonName);
            var translation = await _translatorClient.GetTranslationAsync(pokemonRace.Description);
            return new PokemonTranslationReadModel(pokemonRace.Name, pokemonRace.Description, translation);
        }
    }
}