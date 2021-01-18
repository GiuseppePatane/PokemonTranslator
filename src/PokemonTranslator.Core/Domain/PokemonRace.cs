using System;

namespace PokemonTranslator.Core.Domain
{
    public class PokemonRace
    {
        private PokemonRace(int id, string name, string description)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            Id = id;
            Name = name;
            Description = description;
        }

        public int Id { get; }
        public string Name { get; }
        public string Description { get; }

        public static PokemonRace Create(int id, string name, string description)
        {
            return new PokemonRace(id, name, description);
        }
    }
}