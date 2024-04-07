using Newtonsoft.Json;
using Pokedex.DM;
using static System.Net.Mime.MediaTypeNames;

namespace Pokedex.Services
{
    public class TranslatorService
    {
        private readonly HttpClient _httpClient;

        public TranslatorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Pokemon GetTranslatedPokemonInfo(Pokemon pokemon)
        {
            try
            {
                //PokemonService _pokemonService = new PokemonService(new HttpClient());
                //Pokemon pokemon = _pokemonService.GetPokemonInfo(pokemonName);

                var translator = pokemon.Habitat.ToLower() == "cave" || pokemon.IsLegendary ? "yoda" : "shakespeare";

                _httpClient.BaseAddress = new Uri($"https://api.funtranslations.com/translate/{translator}.json?text={pokemon.Description}");
                var response = _httpClient.GetAsync(_httpClient.BaseAddress).Result;

                var responseContent = response.Content.ReadAsStringAsync().Result;
                var dynamicObject = JsonConvert.DeserializeObject<dynamic>(responseContent)!;

                if (dynamicObject != null)
                {
                    if (dynamicObject.ContainsKey("contents") && dynamicObject.contents != null)
                        pokemon.Description = dynamicObject.contents.translated ?? "";

                    return pokemon;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return new Pokemon();
        }
    }
}
