using Microsoft.AspNetCore.Mvc;
using Pokedex.DM;
using Pokedex.Services;

namespace Pokedex.Controllers
{
    [Route("pokemon")]
    public class PokemonController : ControllerBase
    {
        [HttpGet("{pokemonName}")]
        public IActionResult GetPokemonInfo(string pokemonName)
        {
            PokemonService _pokemonService = new PokemonService(new HttpClient());
            Pokemon pokemon = _pokemonService.GetPokemonInfo(pokemonName);

            if (!String.IsNullOrEmpty(pokemon.Name))
                return Ok(pokemon);
            else
                return BadRequest("Pokemon not found");
        }

        [HttpGet("translated/{pokemonName}")]
        public IActionResult GetTranslatedPokemonInfo(string pokemonName)
        {
            PokemonService _pokemonService = new PokemonService(new HttpClient());
            Pokemon pokemon = _pokemonService.GetPokemonInfo(pokemonName);

            if (!String.IsNullOrEmpty(pokemon.Name))
            {
                TranslatorService _translatorService = new TranslatorService(new HttpClient());
                pokemon = _translatorService.GetTranslatedPokemonInfo(pokemon);
                return Ok(pokemon);
            }
            else
                return BadRequest("Pokemon not found");
        }
    }
}
