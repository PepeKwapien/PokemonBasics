using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Tests.ExternalApiHandler.Helpers
{
    [TestClass]
    public class RequesterHelperTests
    {
        private Mock<HttpMessageHandler> _handler;
        private string _baseAddress;
        private CountDto _countDto1;
        private CountDto _countDto2;
        private PokemonTypeDto _pokemonTypeDto;

        [TestInitialize]
        public void Initialize()
        {
            _handler = new Mock<HttpMessageHandler>();
            _baseAddress = "http://example.com";
            _countDto1 = new CountDto
            {
                count = 3,
                next = "1",
                results = new Url[]
                {
                    new Url
                    {
                        url = $"{_baseAddress}/1"
                    },
                     new Url
                    {
                        url = $"{_baseAddress}/2"
                    },
                      new Url
                    {
                        url = $"{_baseAddress}/3"
                    },
                }
            };
            _countDto2 = new CountDto
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
            
            _pokemonTypeDto = new PokemonTypeDto()
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
        }

        [TestMethod]
        public async Task GetDeserializesObject()
        {
            // Arrange
            string pokemonTypeDtoJson = JsonConvert.SerializeObject(_pokemonTypeDto);

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
                BaseAddress = new Uri(_baseAddress)
            };

            // Act
            var result = await RequesterHelper.Get<PokemonTypeDto>(httpClient, "any path");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_pokemonTypeDto.name, result.name);
            Assert.AreEqual(_pokemonTypeDto.names.Length, result.names.Length);
            Assert.AreEqual(_pokemonTypeDto.damage_relations.half_damage_to.Length, result.damage_relations.half_damage_to.Length);
        }

        [TestMethod]
        public async Task GetCollectionUrls_GetsCorrectNumberOfUrls()
        {
            // Arrange
            _countDto1.next = "2";
            _handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", ItExpr.Is<HttpRequestMessage>(r => r.RequestUri.AbsolutePath.Equals("/1")), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(_countDto1), Encoding.UTF8, "application/json")
                });
            
            _handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", ItExpr.Is<HttpRequestMessage>(r => r.RequestUri.AbsolutePath.Equals("/2")), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(_countDto2), Encoding.UTF8, "application/json")
                });

            // Create a new HttpClient with the mock HttpMessageHandler
            var httpClient = new HttpClient(_handler.Object)
            {
                BaseAddress = new Uri(_baseAddress)
            };

            // Act
            var result = await RequesterHelper.GetCollectionUrls(httpClient, "1");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_countDto1.count + _countDto2.count, result.Count);
        }

        [TestMethod]
        public async Task GetCollection_IteratesEveryUrl()
        {
            // Arrange
            List<string> urls = _countDto1.results.Select(result => result.url).ToList();
            _handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(_pokemonTypeDto), Encoding.UTF8, "application/json")
                });

            var httpClient = new HttpClient(_handler.Object)
            {
                BaseAddress = new Uri(_baseAddress)
            };

            // Act
            var result = await RequesterHelper.GetCollection<PokemonTypeDto>(httpClient, urls);

            // Assert
            Assert.AreEqual(urls.Count, _handler.Invocations.Count(client => client.Method.Name == "SendAsync"));
            foreach (var invocation in _handler.Invocations)
            {
                if (invocation.Method.Name == "SendAsync")
                {
                    HttpRequestMessage request = (HttpRequestMessage)invocation.Arguments[0];
                    Assert.IsTrue(urls.Contains(request.RequestUri.ToString()));
                }
            }
        }
    }
}
