using System.Threading.Tasks;
using PokemonTranslator.Core.Exceptions;
using PokemonTranslator.Core.Interfaces;
using PokemonTranslator.Core.ReadModels;

namespace PokemonTranslator.Core.Services
{
    public class TranslatorService :ITranslatorService
    {
        private readonly IPokemonClient _pokemonClient;
        // private readonly ITranslatorClient _translatorClient;
        public TranslatorService(IPokemonClient pokemonClient)
        {
            _pokemonClient = pokemonClient;
        }

        public async Task<PokemonTranslationReadModel> GetPokemonTranslationAsync(string pokemonName)
        {
            var pokemonRace = await _pokemonClient.GetPokemonRaceAsync(pokemonName);
            return new PokemonTranslationReadModel() {Name = pokemonRace.Name,Description = pokemonRace.Description};

        }
    }
}