using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace CreditService.WebApi.Extensions
{
    public static class HttpExtensions
    {
        public static async Task<IActionResult> ToHttpResponse<T>(this Task<Result<T>> task)
        {
            try
            {
                var result = await task;

                return result.IsSuccess ?
                    (IActionResult)new OkObjectResult(result.Value) :
                    (IActionResult)new BadRequestObjectResult(result.Error);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
