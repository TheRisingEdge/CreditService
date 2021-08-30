using System.Threading;
using System.Threading.Tasks;
using CreditService.Domain;
using CreditService.Domain.CreditDecisionMaker;
using CSharpFunctionalExtensions;
using MediatR;

namespace CreditService.Application.ApplyForCredit
{
    public class ApplyForCreditHandler : IRequestHandler<ApplyForCreditRequest, Result<ApplyForCreditResponse>>
    {
        private readonly CreditDecisionMaker _creditDecisionMaker;

        public ApplyForCreditHandler(CreditDecisionMaker creditDecisionMaker)
        {
            _creditDecisionMaker = creditDecisionMaker;
        }

        public Task<Result<ApplyForCreditResponse>> Handle(ApplyForCreditRequest request, CancellationToken cancellationToken)
        {
            var creditAmount = CreditAmount.TryCreate(request.CreditAmount);
            var repaymentTerm = RepaymentTerm.TryCreate(request.RepaymentTerm);
            var preexistingCreditAmount = CreditAmount.TryCreate(request.PreexistingCreditAmount);

            var response = Result.Combine(creditAmount, repaymentTerm, preexistingCreditAmount)
                .Map(() => MakeDecisionRequestFor(creditAmount, repaymentTerm, preexistingCreditAmount))
                .Map(decisionRequest => _creditDecisionMaker.MakeDecisionFor(decisionRequest))
                .Map(decision => ApplyForCreditResponse.MapFrom(decision));

            return Task.FromResult(response);
        }

        private CreditDecisionRequest MakeDecisionRequestFor(
            Result<CreditAmount> creditAmount,
            Result<RepaymentTerm> repaymentTerm,
            Result<CreditAmount> preexistingCreditAmount) =>
            new CreditDecisionRequest(
                creditAmount.Value,
                repaymentTerm.Value,
                preexistingCreditAmount.Value
            );
    }
}
