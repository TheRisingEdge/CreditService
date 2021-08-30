using CSharpFunctionalExtensions;
using CreditService.Domain;

namespace CreditService.Application
{
    public class ApplyForCreditResponse
    {
        public bool IsSuccessful { get; set; }
        public float? InterestRate { get; set; }
        public string RejectionReason { get; set; }

        private static ApplyForCreditResponse Ok(InterestRate interestRate) =>
            new ApplyForCreditResponse()
            {
                IsSuccessful = true,
                InterestRate = interestRate.Value
            };

        private static ApplyForCreditResponse Rejection(string reason) =>
            new ApplyForCreditResponse()
            {
                IsSuccessful = false,
                RejectionReason = reason
            };

        public static ApplyForCreditResponse MapFrom(CreditDecision creditDecision)
        {
            return creditDecision.EitherInterestRateOrRejectionReason
                .Map(interestRate => Ok(interestRate))
                .OnFailureCompensate(reason => Rejection(reason))
                .Value;
        }
    }
}
