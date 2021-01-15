using System;

namespace PokemonTranslator.Core.Exceptions
{
    [Serializable]
    public abstract class CustomException : Exception
    {
        protected CustomException(string message) : base(message)
        {
        }

        protected CustomException(string code, string message) : base(message)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));
            Code = code;
        }

        public string Code { get; }
    }
}