using Microsoft.AspNetCore.Mvc;
using Pokedex.DM;
using Pokedex.Services;

namespace Pokedex.Controllers
{
    [Route("pokemon")]
    public class PokemonController : ControllerBase
    {
        [HttpGet("{pokemonName}")]
        public async Task<IActionResult> GetPokemonInfo(string pokemonName)
        {
            PokemonService pokemonService = new PokemonService(new HttpClient());
            Pokemon pokemon = await pokemonService.GetPokemonInfo(pokemonName);

            if (!String.IsNullOrEmpty(pokemon.Name))
                return Ok(pokemon);
            else
                return NotFound("Pokemon not found");
        }

        [HttpGet("translated/{pokemonName}")]
        public async Task<IActionResult> GetTranslatedPokemonInfo(string pokemonName)
        {
            PokemonService pokemonService = new PokemonService(new HttpClient());
            Pokemon pokemon = await pokemonService.GetPokemonInfo(pokemonName);

            if (!String.IsNullOrEmpty(pokemon.Name))
            {
                TranslatorService translatorService = new TranslatorService(new HttpClient());
                pokemon = await translatorService.GetTranslatedPokemonInfo(pokemon);
                return Ok(pokemon);
            }
            else
                return NotFound("Pokemon not found");
        }
    }
}
