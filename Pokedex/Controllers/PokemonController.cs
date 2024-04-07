using Microsoft.AspNetCore.Mvc;
using Pokedex.DM;
using Pokedex.Services;

namespace Pokedex.Controllers
{
    [Route("api/pokemon")]
    public class PokemonController : ControllerBase
    {
        [HttpGet("{pokemonName}")]
        public IActionResult GetPokemonInfo(string pokemonName)
        {
            PokemonService _pokemonService = new PokemonService(new HttpClient());

            Pokemon pokemon = _pokemonService.GetPokemonInfo(pokemonName);

            if (!String.IsNullOrEmpty(pokemon.Name))
            {
                return Ok(pokemon);
            }
            else
            {
                return BadRequest("Pokemon not found"); 
            }
        }
    }
}
