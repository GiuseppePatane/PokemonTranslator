using System;

namespace PokemonTranslator.Core.Domain
{
    public class PokemonRace
    {
        public static PokemonRace Create(int id, string name, string description)
        {
            return new PokemonRace(id, name, description);
        }
        private PokemonRace(int id, string name, string description)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            Id = id;
            Name = name;
            Description = description;
        }

        public  int Id { get; private set; }
        public  string Name { get; private set; }
        public  string Description { get; private set; }
    }
}