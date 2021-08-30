using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CreditService.Tests
{
    public static class HttpTestExtensions
    {
        public static async Task<T> ExpectingOK<T>(this Task<IActionResult> task) where T: class {
            var result = await task;

            var value = result
                .Should()
                .BeOfType<OkObjectResult>()
                .Which.Value;

            return value
                .Should()
                .BeOfType<T>()
                .Which;
        }
    }
}
