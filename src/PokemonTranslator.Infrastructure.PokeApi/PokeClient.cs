using System.Linq;
using System.Threading.Tasks;
using PokeApiNet;
using PokemonTranslator.Core.Domain;
using PokemonTranslator.Core.Interfaces;

namespace PokemonTranslator.Infrastructure.PokeApi
{
    public class PokeClient : IPokemonClient
    {
        private readonly PokeApiClient _pokeApiClient;

        public PokeClient(PokeApiClient pokeApiClient)
        {
            _pokeApiClient = pokeApiClient;
        }

        public async Task<PokemonRace> GetPokemonRaceAsync(string pokemon)
        {
            var pokemonSpecies = await _pokeApiClient.GetResourceAsync<PokemonSpecies>(pokemon);

            return pokemonSpecies.MapToPokemonRace();
        }
    }

    public static class Mapper
    {
        public static PokemonRace MapToPokemonRace(this PokemonSpecies pokemonSpecies, string language = "en")
        {
            var description = pokemonSpecies.FlavorTextEntries
                .Where(flavorTexts => flavorTexts.Language.Name == language)
                .Select(f => f.FlavorText);

            return new PokemonRace()
            {
                Id = pokemonSpecies.Id,
                Name = pokemonSpecies.Name,
                Description = "description"
            };
        }
    }
}