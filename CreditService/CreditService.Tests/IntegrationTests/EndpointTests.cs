using CreditService.WebApi;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace CreditService.Tests
{
    public class EndpointTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public EndpointTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/creditdecision?creditamount=2000&repaymentterm=23")]
        [InlineData("/creditdecision?creditamount=2000&repaymentterm=23&preexistingcreditamount=1000")]
        public async Task Should_ReturnOkResult_When_Called(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            response.Content.Headers.ContentType.ToString()
                .Should().Be("application/json; charset=utf-8");
        }
    }
}
