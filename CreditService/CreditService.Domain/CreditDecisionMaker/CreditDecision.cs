using CSharpFunctionalExtensions;

namespace CreditService.Domain.CreditDecisionMaker
{
    public struct CreditDecision
    {
        public Result<InterestRate> EitherInterestRateOrRejectionReason { get; private set; }

        public bool IsSuccess => EitherInterestRateOrRejectionReason.IsSuccess;
        public InterestRate InterestRate => EitherInterestRateOrRejectionReason.Value;
        public string RejectionReason => EitherInterestRateOrRejectionReason.Error;

        private CreditDecision(Result<InterestRate> rateOrRejection)
        {
            EitherInterestRateOrRejectionReason = rateOrRejection;
        }

        public static CreditDecision Ok(InterestRate interestRate) =>
            new CreditDecision(Result.Success(interestRate));

        public static CreditDecision Rejection(string reason) =>
            new CreditDecision(Result.Failure<InterestRate>(reason));
    }
}
