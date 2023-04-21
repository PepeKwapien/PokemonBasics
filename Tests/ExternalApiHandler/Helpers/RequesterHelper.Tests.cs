using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.ExternalApiHandler.Helpers
{
    [TestClass]
    public class RequesterHelperTests
    {
        private Mock<HttpMessageHandler> _handler;

        [TestInitialize]
        public void Initialize()
        {
            _handler = new Mock<HttpMessageHandler>();
        }

        [TestMethod]
        public async Task GetDeserializesObject()
        {
            // Arrange
            PokemonTypeDto pokemonTypeDto = new PokemonTypeDto()
            {
                name = "ice",
                names = new NameWithLanguage[]
                    {
                        new NameWithLanguage()
                        {
                            name = "Ice",
                            language = new Name() { name = "en" }
                        },
                        new NameWithLanguage()
                        {
                            name = "Icu",
                            language = new Name() { name = "jp" }
                        }
                    },
                damage_relations = new TypeDamageRelations()
                {
                    no_damage_from = new Name[0],
                    no_damage_to = new Name[0],
                    half_damage_from = new Name[0],
                    half_damage_to = new Name[] { new Name() { name = "fire" }, new Name() { name = "water" }, new Name() { name = "steel" } },
                    double_damage_from = new Name[0],
                    double_damage_to = new Name[0]
                }
            };
            string pokemonTypeDtoJson = JsonConvert.SerializeObject(pokemonTypeDto);

            _handler.Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(pokemonTypeDtoJson, Encoding.UTF8, "application/json")
                    });

            // Create a HttpClient with the mock HttpMessageHandler
            var httpClient = new HttpClient(_handler.Object)
            {
                BaseAddress = new Uri("https://api.example.com/")
            };

            // Act
            var result = await RequesterHelper.Get<PokemonTypeDto>(httpClient, "any path");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(pokemonTypeDto.name, result.name);
            Assert.AreEqual(pokemonTypeDto.names.Length, result.names.Length);
            Assert.AreEqual(pokemonTypeDto.damage_relations.half_damage_to.Length, result.damage_relations.half_damage_to.Length);
        }

        
    }
}
