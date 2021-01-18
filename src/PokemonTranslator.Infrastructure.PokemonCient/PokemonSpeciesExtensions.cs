using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var description = ParseFlavorTextEntries(pokemonSpecies?.FlavorTextEntries, pokemonSpecies?.Name,
                language);
            Debug.Assert(pokemonSpecies != null, nameof(pokemonSpecies) + " != null");
            return PokemonRace.Create(pokemonSpecies.Id, pokemonSpecies.Name, description);
        }

        /// <summary>
        /// Parse the List<PokemonSpeciesFlavorTexts>?flavorTextsList
        /// and then returns the first element thant start or contains the pokemon name.
        /// Otherwise, return the first element of the list or an empty string
        /// </summary>
        /// <param name="flavorTextsList"></param>
        /// <param name="name"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        private static string ParseFlavorTextEntries(List<PokemonSpeciesFlavorTexts>? flavorTextsList, string? name,
            string language)
        {
       
            if (flavorTextsList == null || !flavorTextsList.Any() || string.IsNullOrEmpty(name)) return string.Empty;
            var description = flavorTextsList
                .Where(flavorTexts => flavorTexts.Language.Name == language)
                .SkipWhile(d => !d.FlavorText.StartsWith(name,StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault()?.FlavorText.RemoveInvalidCharacters() ?? flavorTextsList
                .Where(flavorTexts => flavorTexts.Language.Name == language)
                .SkipWhile(d => !d.FlavorText.Contains(name,StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault()?.FlavorText.RemoveInvalidCharacters();

            if (string.IsNullOrWhiteSpace(description))
            {
                description = flavorTextsList.First()?.FlavorText.RemoveInvalidCharacters();
            }
            return description ?? string.Empty;
        }

        private static string? RemoveInvalidCharacters(this string? text) => string.IsNullOrWhiteSpace(text) ? null : Regex.Replace(text, @"\t|\n|\r|\f", " ").Trim();
    }
}