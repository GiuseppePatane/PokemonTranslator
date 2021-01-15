using System.Threading.Tasks;

namespace PokemonTranslator.Core.Interfaces
{
    public interface ITranslatorClient
    {
        public Task<string> GetTranslationAsync(string text);
    }
}