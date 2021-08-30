using CreditService.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CreditService.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditDecisionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CreditDecisionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<IActionResult> Get([FromQuery] ApplyForCreditRequest request)
        {
            return _mediator.Send(request).ToHttpResponse();
        }
    }
}
