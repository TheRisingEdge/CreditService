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
            return Result.Success(months)
                .Ensure(m => m > 0, "Repayment in months must be positive")
                .Map(m => new RepaymentTerm(m));
        }
    }
}
