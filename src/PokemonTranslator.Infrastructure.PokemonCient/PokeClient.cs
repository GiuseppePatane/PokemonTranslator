using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using PokeApiNet;
using PokemonTranslator.Core.Domain;
using PokemonTranslator.Core.Exceptions;
using PokemonTranslator.Core.Interfaces;

namespace PokemonTranslator.Infrastructure.PokemonCient
{
    public class PokeClient : IPokemonClient
    {
        private readonly PokeApiClient _pokeApiClient;

        public PokeClient(PokeApiClient pokeApiClient)
        {
            _pokeApiClient = pokeApiClient;
        }

        /// <summary>
        ///     Get Pokemon Race Information using  PokeApiClient
        ///     status code not found  will throw  an PokemonNotFoundException
        /// </summary>
        /// <param name="pokemon"></param>
        /// <returns></returns>
        /// <exception cref="PokemonNotFoundException"></exception>
        public async Task<PokemonRace> GetPokemonRaceAsync(string pokemon)
        {
            try
            {
                var pokemonSpecies = await _pokeApiClient.GetResourceAsync<PokemonSpecies>(pokemon.ToLower().Trim());
                return pokemonSpecies?.MapToPokemonRace();
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                throw new PokemonNotFoundException();
            }
        }
    }
}