using CreditService.WebApi;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
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
        public async Task Should_ReturnOk_WhenCalledCorrectly(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            response.Content.Headers.ContentType.ToString()
                .Should().Be("application/json; charset=utf-8");
        }

        [Theory]
        [InlineData("/creditdecision?creditamount=2000&repaymentterm=asdf")]
        [InlineData("/creditdecision?creditamount=a34&repaymentterm=23&preexistingcreditamount=1000")]
        public async Task Should_Return400_When_QueryParamsAreWrong(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
