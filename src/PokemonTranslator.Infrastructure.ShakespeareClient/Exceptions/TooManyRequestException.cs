using PokemonTranslator.Core.Exceptions;

namespace PokemonTranslator.Infrastructure.ShakespeareClient.Exceptions
{
    public class TooManyRequestException :CustomException
    {
        public TooManyRequestException(string message) : base("TooManyRequestErrorKey", message)
        {
        }
    }
}