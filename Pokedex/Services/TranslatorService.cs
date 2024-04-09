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

        public Pokemon GetTranslatedPokemonInfo(Pokemon pokemon)
        {
            try
            {
                // Check if the Pokemon's habitat is a cave or if it is a legendary Pokemon, and apply Yoda's translation, otherwise apply Shakespeare's translation
                var translator = pokemon.Habitat.ToLower() == "cave" || pokemon.IsLegendary ? "yoda" : "shakespeare";

                _httpClient.BaseAddress = new Uri($"https://api.funtranslations.com/translate/{translator}.json?text={pokemon.Description}");

                // Call FunTranslations API
                var response = _httpClient.GetAsync(_httpClient.BaseAddress).Result;

                // Get response content as a JSON string
                var responseContent = response.Content.ReadAsStringAsync().Result;

                // Deserialize JSON string into Dynamic Object
                var dynamicObject = JsonConvert.DeserializeObject<dynamic>(responseContent)!;

                if (dynamicObject != null)
                {
                    // Get translated description
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
