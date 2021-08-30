using CSharpFunctionalExtensions;

namespace CreditService.Domain
{
    public struct RepaymentTerm
    {
        public int Months { get; private set; }

        private RepaymentTerm(int months)
        {
            Months = months;
        }

        public static Result<RepaymentTerm> TryCreate(int months)
        {
            return Result.SuccessIf(months > 0,
                new RepaymentTerm(months),
                "Repayment in months must be positive");
        }
    }
}
