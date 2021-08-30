using CSharpFunctionalExtensions;
using MediatR;

namespace CreditService.Application.ApplyForCredit
{
    public class ApplyForCreditRequest : IRequest<Result<ApplyForCreditResponse>>
    {
        public int CreditAmount { get; set; }
        public int RepaymentTerm { get; set; }
        public int? PreexistingCreditAmount { get; set; }
    }
}
