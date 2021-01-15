using System.Threading.Tasks;
using PokemonTranslator.Core.Domain;

namespace PokemonTranslator.Core.Interfaces
{
    public interface IPokemonClient
    {
        /// <summary>
        ///     Get the pokemon race description of the given pokemon name
        /// </summary>
        /// <param name="pokemon"></param>
        /// <returns></returns>
        public Task<PokemonRace> GetPokemonRaceAsync(string pokemonName);
    }
}