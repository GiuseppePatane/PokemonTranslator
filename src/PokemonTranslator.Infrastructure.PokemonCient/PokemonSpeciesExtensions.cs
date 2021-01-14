using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PokeApiNet;
using PokemonTranslator.Core.Domain;
using PokemonTranslator.Core.Exceptions;

namespace PokemonTranslator.Infrastructure.PokemonCient
{
    public static class PokemonSpeciesExtensions
    {
        /// <summary>
        /// Create a new PokemonRace with the info provider by the PokemonSpecies
        /// throw an PokemonNotFoundException it the given entity is null or empty
        /// </summary>
        /// <param name="pokemonSpecies"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        /// <exception cref="PokemonNotFoundException"></exception>
        public static PokemonRace MapToPokemonRace(this PokemonSpecies? pokemonSpecies, string language = "en")
        {
            if (pokemonSpecies == null || pokemonSpecies.Id <= 0) throw  new PokemonNotFoundException();
            
            var descriptions = (pokemonSpecies.FlavorTextEntries==null ||!pokemonSpecies.FlavorTextEntries.Any())
                ? new List<string>() 
                : pokemonSpecies.FlavorTextEntries
                    .Where(flavorTexts => flavorTexts.Language.Name == language)
                    .Take(1)
                    .Select(f =>  Regex.Replace(f.FlavorText, @"\t|\n|\r|\f", " ").Trim())
                    .ToList();
            var description = descriptions.Any() ? string.Join( "", descriptions) : string.Empty;
            return PokemonRace.Create(pokemonSpecies.Id, pokemonSpecies.Name, description);
        }
    }
}