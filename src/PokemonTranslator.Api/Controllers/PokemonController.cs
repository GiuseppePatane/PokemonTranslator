using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokemonTranslator.Api.Models;
using PokemonTranslator.Core.Interfaces;
using PokemonTranslator.Core.ReadModels;

namespace PokemonTranslator.Api.Controllers
{
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly ITranslatorService _translatorService;

        public PokemonController(ITranslatorService translatorService)
        {
            _translatorService = translatorService;
        }

        [HttpGet("pokemon/{name}")]
        public async Task<PokemonDescriptionResponse> GetPokemonDescription(string name)
        {
           var result=   await _translatorService.GetPokemonTranslationAsync(name);
           return new PokemonDescriptionResponse
           {
               Name = result.Name,
               Description = result.Description
           };
        }
    }
    
 
}