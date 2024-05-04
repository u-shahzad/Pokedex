using Newtonsoft.Json;
using Pokedex.DM;

namespace Pokedex.Services
{
    public class TranslatorService
    {
        private readonly HttpClient _httpClient;

        public TranslatorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Pokemon> GetTranslatedPokemonInfo(Pokemon pokemon)
        {
            // Check if the Pokemon's habitat is a cave or if it is a legendary Pokemon, and apply Yoda's translation, otherwise apply Shakespeare's translation
            var translator = pokemon.Habitat.ToLower() == "cave" || pokemon.IsLegendary ? "yoda" : "shakespeare";

            _httpClient.BaseAddress = new Uri($"https://api.funtranslations.com/translate/{translator}.json?text={pokemon.Description}");

            // Call FunTranslations API
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress);

            response.EnsureSuccessStatusCode();

            // Get response content as a JSON string
            var responseContent = await response.Content.ReadAsStringAsync();

            // Deserialize JSON string into Dynamic Object
            var dynamicObject = JsonConvert.DeserializeObject<dynamic>(responseContent)!;

            if (dynamicObject != null)
            {
                // Get translated description
                if (dynamicObject.ContainsKey("contents") && dynamicObject.contents != null)
                    pokemon.Description = dynamicObject.contents.translated ?? "";
            }

            return pokemon;
        }
    }
}
