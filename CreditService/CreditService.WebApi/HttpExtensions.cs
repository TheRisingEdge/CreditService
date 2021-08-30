using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSharpFunctionalExtensions;

namespace CreditService.WebApi.Controllers
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
