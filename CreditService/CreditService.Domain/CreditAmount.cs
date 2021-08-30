using CSharpFunctionalExtensions;

namespace CreditService.Domain
{
    public struct CreditAmount
    {
        public int Value { get; set; }

        internal CreditAmount(int value)
        {
            Value = value;
        }

        public static Result<CreditAmount> TryCreate(int? amount)
        {
            var amountOrZero = amount.GetValueOrDefault(0);

            return Result.SuccessIf(amountOrZero >= 0,
                new CreditAmount(amountOrZero),
                "Credit amount must be positive");
        }

        public static CreditAmount operator +(CreditAmount a, CreditAmount b) =>
            new CreditAmount(a.Value + b.Value);
    }
}
