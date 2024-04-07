using Newtonsoft.Json;
using Pokedex.DM;
using System.Text.RegularExpressions;

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
                    Pokemon pokemon = new Pokemon();

                    if (dynamicObject != null)
                    {
                        pokemon.Name = dynamicObject.name ?? "";

                        // Search and get Pokemon description in English language
                        foreach (var obj in dynamicObject.flavor_text_entries)
                        {
                            string languageName = obj.language.name ?? "";
                            if (languageName.Equals("en"))
                            {
                                string description = obj.flavor_text ?? "";
                                pokemon.Description = description.Replace("\n", " ").Replace("\f", " ");
                                break;
                            }
                        }

                        var habitat = dynamicObject.habitat;

                        if (habitat != null)
                        {
                            pokemon.Habitat = habitat.name ?? "";
                        }

                        pokemon.IsLegendary = dynamicObject.is_legendary ?? "";
                    }

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
