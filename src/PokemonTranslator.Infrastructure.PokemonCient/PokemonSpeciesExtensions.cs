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
        ///     Create a new PokemonRace with the info provider by the PokemonSpecies
        ///     throw an PokemonNotFoundException it the given entity is null or empty
        /// </summary>
        /// <param name="pokemonSpecies"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        /// <exception cref="PokemonNotFoundException"></exception>
        public static PokemonRace MapToPokemonRace(this PokemonSpecies? pokemonSpecies, string language = "en")
        {
            if (pokemonSpecies == null || pokemonSpecies.Id <= 0) throw new PokemonNotFoundException();
            var description = ParseFlavorTextEntries(pokemonSpecies?.FlavorTextEntries, pokemonSpecies.Name.ToUpper(),
                language);
            return PokemonRace.Create(pokemonSpecies.Id, pokemonSpecies.Name, description);
        }

        private static string ParseFlavorTextEntries(List<PokemonSpeciesFlavorTexts>? flavorTextsList, string name,
            string language)
        {
            if (flavorTextsList == null || !flavorTextsList.Any()) return string.Empty;
            // if the 
            var description = flavorTextsList
                .Where(flavorTexts => flavorTexts.Language.Name == language)
                .SkipWhile(d => !d.FlavorText.StartsWith(name))
                .Select(f => Regex.Replace(f.FlavorText, @"\t|\n|\r|\f", " ").Trim())
                .FirstOrDefault() ?? flavorTextsList
                .Where(flavorTexts => flavorTexts.Language.Name == language)
                .SkipWhile(d => !d.FlavorText.Contains(name))
                .Select(f => Regex.Replace(f.FlavorText, @"\t|\n|\r|\f", " ").Trim())
                .FirstOrDefault();

            return description ?? string.Empty;
        }
    }
}