using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        
        
        /// <summary>
        /// Get the Shakespearean translation of the given pokemon name  
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /pokemon/ditto
        ///     {
        ///        "name": "ditto",
        ///        "description": "description text"
        ///     }
        ///
        /// </remarks>
        /// <param name="name"> the pokemon name</param>
        /// <returns>The pokemon description </returns>
        /// <response code="200">Returns the pokemon description response </response>
        /// <response code="204">pokemon not found</response>
        /// <response code="400">generic error response</response>
        /// <response code="429">translation api limit reached </response>
        /// <response code="500">internal server error</response>
        [Produces("application/json")]
        [HttpGet("pokemon/{name}")]
        [ProducesResponseType(typeof(PokemonDescriptionResponse),200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ErrorsResponse),400)]
        [ProducesResponseType(typeof(ErrorsResponse),429)]
        [ProducesResponseType(typeof(ErrorsResponse),500)]
        public async Task<PokemonDescriptionResponse> GetPokemonDescription(string name)
        {
           var result=   await _translatorService.GetPokemonTranslationAsync(name);
           return new PokemonDescriptionResponse
           {
               Name = result.Name,
               Description = result.Translation
           };
        }
    }
    
 
}