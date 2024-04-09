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
            try
            {
                // For the production environment, we can save the BaseUri as a key in a web.config or appsettings.json file
                _httpClient.BaseAddress = new Uri($"https://pokeapi.co/api/v2/pokemon-species/{pokemonName.ToLower()}/");

                // Call PokéAPI
                var response = _httpClient.GetAsync(_httpClient.BaseAddress).Result;

                if (response.IsSuccessStatusCode)
                {
                    // Get response content as a JSON string
                    var responseContent = response.Content.ReadAsStringAsync().Result;

                    // Deserialize JSON string into Dynamic Object
                    var dynamicObject = JsonConvert.DeserializeObject<dynamic>(responseContent)!;

                    Pokemon pokemon = new Pokemon();

                    if (dynamicObject != null)
                    {
                        // Get Pokemon name
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

                        // Habitat
                        if (habitat != null)
                            pokemon.Habitat = habitat.name ?? "";
                        else
                            pokemon.Habitat = "";

                        // IsLegendary
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
