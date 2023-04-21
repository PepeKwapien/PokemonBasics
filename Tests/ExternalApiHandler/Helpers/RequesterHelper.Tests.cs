using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Moq.Language.Flow;
using Newtonsoft.Json;
using System;
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

        [TestMethod]
        public async Task GetCollectionUrls_GetsCorrectNumberOfUrls()
        {
            // Arrange
            string baseAddress = "http://example.com";
            CountDto countDto1 = new CountDto
            {
                count = 3,
                next = "2",
                results = new Url[]
                {
                    new Url
                    {
                        url = "1"
                    },
                     new Url
                    {
                        url = "2"
                    },
                      new Url
                    {
                        url = "3"
                    },
                }
            };

            CountDto countDto2 = new CountDto
            {
                count = 2,
                next = "",
                results = new Url[]
                {
                    new Url
                    {
                        url = "4"
                    },
                     new Url
                    {
                        url = "5"
                    },
                }
            };
            _handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", ItExpr.Is<HttpRequestMessage>(r => r.RequestUri.AbsolutePath.Equals("/1")), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(countDto1), Encoding.UTF8, "application/json")
                });
            
            _handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", ItExpr.Is<HttpRequestMessage>(r => r.RequestUri.AbsolutePath.Equals("/2")), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(countDto2), Encoding.UTF8, "application/json")
                });

            // Create a new HttpClient with the mock HttpMessageHandler
            var httpClient = new HttpClient(_handler.Object)
            {
                BaseAddress = new Uri(baseAddress)
            };

            // Act
            var result = await RequesterHelper.GetCollectionUrls(httpClient, "1", "notExisting");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(countDto1.count + countDto2.count, result.Count);
        }
    }
}
