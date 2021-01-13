namespace PokemonTranslator.Core.Exceptions
{
    public class PokemonNotFoundException : CustomException
    {
        public PokemonNotFoundException() : base("PokemonNotFoundKey", "Pokemon not found")
        {
        }
    }
}