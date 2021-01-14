using System.Threading.Tasks;
using PokemonTranslator.Core.Domain;
using PokemonTranslator.Core.ReadModels;

namespace PokemonTranslator.Core.Interfaces
{
    public interface ITranslatorClient
    {
        public Task<string> GetTranslation(string text);
    }
}