using PokemonTranslator.Core.Exceptions;

namespace PokemonTranslator.Infrastructure.ShakespeareClient.Exceptions
{
    public class ShakespeareClientException :CustomException
    {
        public ShakespeareClientException(string message) : base("ShakespeareClientErrorKey", message)
        {
        }
    }
}