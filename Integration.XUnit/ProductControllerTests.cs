using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TestAPI;
using Xunit;

namespace Integration.XUnit
{
    public class ProductControllerTests : IClassFixture<StartupFixture<Startup>>
    {
        private HttpClient Client;

        public ProductControllerTests(StartupFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }



        [Theory]
        [InlineData("Hotel")]
        [InlineData("Tour")]
        [InlineData("Flight")]
        public async Task Test_GetById(string pid)
        {
            var request = $"/api/Product/{pid}";
            var response = await Client.GetAsync(request);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var responseProduct = JsonConvert.DeserializeObject<string>(stringResponse);

            Assert.Equal(pid, responseProduct);

        }
        [Theory]
        [InlineData("DomesticHotel", HttpStatusCode.OK)]
        [InlineData("DomesticTour", HttpStatusCode.OK)]
        public async Task Test_Put(string pid, HttpStatusCode expected)
        {
            var url = $"/api/Product";
            var request = new
            {
                Url = url,
                Body = new
                {
                    id = pid
                }
            };
            var response = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();
            Assert.Equal(expected, response.StatusCode);
        }

        [Theory]
        [InlineData("Hotel", HttpStatusCode.OK)]
        [InlineData("Tour", HttpStatusCode.OK)]
        [InlineData("UnKnowProduct", HttpStatusCode.NotFound)]

        public async Task Test_Delete(string pid, HttpStatusCode expected)
        {
            // Arrange
            var url = $"/api/Product/{pid}";

            // Act
            var response = await Client.DeleteAsync(url);

            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(expected, response.StatusCode);
        }

    }
}
