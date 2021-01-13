using System.Threading.Tasks;
using PokemonTranslator.Core.Domain;

namespace PokemonTranslator.Core.Interfaces
{
    public interface IPokemonClient
    {
        public Task<PokemonRace> GetPokemonRaceAsync(string pokemon);
    }
}