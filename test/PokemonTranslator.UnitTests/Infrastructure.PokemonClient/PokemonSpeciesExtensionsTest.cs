using System.Collections.Generic;
using FluentAssertions;
using PokeApiNet;
using PokemonTranslator.Core.Exceptions;
using  PokemonTranslator.Infrastructure.PokemonCient;
using Xunit;

public class PokemonSpeciesExtensionsTest
{
    [Fact]
    public void MapToPokemonRace_With_a_Null_PokemonSpecies_Should_Throw_a_PokemonNotFoundException()
    {
        PokemonSpecies pokemonSpecies = null;
        Assert.Throws<PokemonNotFoundException>(() =>
        {
            pokemonSpecies.MapToPokemonRace();
        });
    }
    [Fact]
    public void MapToPokemonRace_With_An_Empty_PokemonSpecies_Should_Throw_a_PokemonNotFoundException()
    { 
        var pokemonSpecies = new PokemonSpecies();
        Assert.Throws<PokemonNotFoundException>(() =>
        {
            pokemonSpecies.MapToPokemonRace();
        });
    }
    [Fact]
    public void MapToPokemonRace_With_a_PokemonSpecies_With_A_Empty_DescriptionList_Should_Return_An_Entity()
    { 
        var pokemonSpecies = new PokemonSpecies(){Id = 1 ,Name = "ditto"};
     
        var  pokemonRace =   pokemonSpecies.MapToPokemonRace();
        pokemonRace.Should().NotBeNull();
        pokemonRace.Id.Should().Be(1);
        pokemonRace.Name.Should().Be("ditto");

    }
    [Fact]
    public void MapToPokemonRace_With_a_PokemonSpecies_With_Descriptions_Thant_Starts_With_The_Name_Should_Return_An_Entity_With_That_Description()
    {
        var id = 1;
        var name = "ditto";
        var validDescription = $"{name} bla bla bla";
        var validDescription2 = $"{name} sdfsfdsfsdfsdfssdfsd";
        var invalidDescription1=$"bla bla {name} bla bla bla";
        var defaultLanguage = new NamedApiResource<Language>()
        {
            Name = "en"
        };
        var pokemonSpecies = new PokemonSpecies()
        {
            Id = id ,
            Name = name,
            FlavorTextEntries = new List<PokemonSpeciesFlavorTexts>()
            {
                new PokemonSpeciesFlavorTexts()
                {
                    FlavorText = validDescription,
                    Language = defaultLanguage
                },
                new PokemonSpeciesFlavorTexts()
                {
                    FlavorText = validDescription2,
                    Language = defaultLanguage
                },
                new PokemonSpeciesFlavorTexts()
                {
                    FlavorText = invalidDescription1,
                    Language = defaultLanguage
                }
            }
        };
     
        var  pokemonRace =   pokemonSpecies.MapToPokemonRace();
        pokemonRace.Id.Should().Be(id);
        pokemonRace.Name.Should().Be(name);
        pokemonRace.Description.Should().Be(validDescription);
    }
    
    [Fact]
    public void MapToPokemonRace_With_a_PokemonSpecies_With_Descriptions_Thant_Contains_The_Name_Should_Return_An_Entity_With_That_Description()
    {
        var id = 1;
        var name = "ditto";
        var validDescription=$"bla bla {name} bla bla bla";
        var validDescription1=$"sdfsdfdsfsdf {name} bla bla bla";
        var invalidDescription = "dsfsfsdfsdfsdfsdasad";
        var defaultLanguage = new NamedApiResource<Language>()
        {
            Name = "en"
        };
        var pokemonSpecies = new PokemonSpecies()
        {
            Id = id ,
            Name = name,
            FlavorTextEntries = new List<PokemonSpeciesFlavorTexts>()
            {
                new PokemonSpeciesFlavorTexts()
                {
                    FlavorText = validDescription,
                    Language = defaultLanguage
                },
                new PokemonSpeciesFlavorTexts()
                {
                    FlavorText = validDescription1,
                    Language = defaultLanguage
                },
                new PokemonSpeciesFlavorTexts()
                {
                    FlavorText = invalidDescription,
                    Language = defaultLanguage
                }
            }
        };
     
        var  pokemonRace =   pokemonSpecies.MapToPokemonRace();
        pokemonRace.Id.Should().Be(id);
        pokemonRace.Name.Should().Be(name);
        pokemonRace.Description.Should().Be(validDescription);
    }
    
    [Fact]
    public void MapToPokemonRace_With_A_PokemonSpecies_With_Descriptions_With_No_Name_Should_Return_An_Entity_With_That_Description()
    {
        var id = 1;
        var name = "ditto";
        var validDescription=$"dsfsdfdsfsd";
        var validDescription1=$"sdfsdfdsfsdf1";
        var defaultLanguage = new NamedApiResource<Language>()
        {
            Name = "en"
        };
        var pokemonSpecies = new PokemonSpecies()
        {
            Id = id ,
            Name = name,
            FlavorTextEntries = new List<PokemonSpeciesFlavorTexts>()
            {
                new PokemonSpeciesFlavorTexts()
                {
                    FlavorText = validDescription,
                    Language = defaultLanguage
                },
                new PokemonSpeciesFlavorTexts()
                {
                    FlavorText = validDescription1,
                    Language = defaultLanguage
                }
            }
        };
        var  pokemonRace =   pokemonSpecies.MapToPokemonRace();
        pokemonRace.Id.Should().Be(id);
        pokemonRace.Name.Should().Be(name);
        pokemonRace.Description.Should().Be(validDescription);
    }
    
    
}