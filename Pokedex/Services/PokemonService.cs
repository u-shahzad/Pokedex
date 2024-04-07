using Newtonsoft.Json;
using Pokedex.DM;

namespace Pokedex.Services
{
    public class PokemonService
    {
        private readonly HttpClient _httpClient;

        public PokemonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Pokemon GetPokemonInfo(string pokemonName)
        {
            _httpClient.BaseAddress = new Uri($"https://pokeapi.co/api/v2/pokemon-species/{pokemonName.ToLower()}/");

            var response = _httpClient.GetAsync(_httpClient.BaseAddress).Result;

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;

                    var dynamicObject = JsonConvert.DeserializeObject<dynamic>(responseContent)!;

                    var pokemon = new Pokemon()
                    {
                        Name = dynamicObject.name,
                        Description = dynamicObject.flavor_text_entries[0].flavor_text,
                        Habitat = dynamicObject.habitat.name,
                        IsLegendary = dynamicObject.is_legendary
                    };

                    return pokemon;
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return new Pokemon();
        }
    }
}
