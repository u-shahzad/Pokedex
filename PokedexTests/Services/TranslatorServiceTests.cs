using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pokedex.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Services.Tests
{
    [TestClass()]
    public class TranslatorServiceTests
    {
        [TestMethod()]
        public void GetTranslatedPokemonInfoTest()
        {
            var httpClient = new HttpClient();
            var _pokemonService = new PokemonService(httpClient);

            var pokemon = _pokemonService.GetPokemonInfo("mewtwo");

            TranslatorService _translatorService = new TranslatorService(new HttpClient());
            pokemon = _translatorService.GetTranslatedPokemonInfo(pokemon);

            // Assert
            Assert.IsNotNull(pokemon);
            Assert.AreEqual("mewtwo", pokemon.Name);
            Assert.AreEqual("Created by a scientist after years of horrific gene splicing and dna engineering experiments,  it was.", pokemon.Description);
            Assert.AreEqual("rare", pokemon.Habitat);
            Assert.AreEqual(true, pokemon.IsLegendary);
        }
    }
}