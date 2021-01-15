using System.Threading.Tasks;
using PokemonTranslator.Core.ReadModels;

namespace PokemonTranslator.Core.Interfaces
{
    public interface ITranslatorService
    {
        Task<PokemonTranslationReadModel> GetPokemonTranslationAsync(string pokemonName);
    }
}