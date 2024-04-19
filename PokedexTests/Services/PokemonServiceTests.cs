using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pokedex.DM;
using Pokedex.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Services.Tests
{
    [TestClass()]
    public class PokemonServiceTests
    {
        [TestMethod()]
        public async void GetPokemonInfoTest()
        {
            var httpClient = new HttpClient();
            var pokemonService = new PokemonService(httpClient);

            var pokemon = await pokemonService.GetPokemonInfo("mewtwo");

            // Assert
            Assert.IsNotNull(pokemon);
            Assert.AreEqual("mewtwo", pokemon.Name);
            Assert.AreEqual("It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.", pokemon.Description);
            Assert.AreEqual("rare", pokemon.Habitat);
            Assert.AreEqual(true, pokemon.IsLegendary);
        }
    }
}