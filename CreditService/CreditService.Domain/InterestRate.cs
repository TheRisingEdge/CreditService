using CSharpFunctionalExtensions;

namespace CreditService.Domain
{
    public struct InterestRate
    {
        public float Value { get; private set; }

        private InterestRate(float value)
        {
            Value = value;
        }

        public static Result<InterestRate> TryCreate(float value)
        {
            return Result.Success(value)
                .Ensure(v => v > 0, "Interest rate must be positive")
                .Ensure(v => v <= 1, "Interest rate must be less than 1")
                .Map(v => new InterestRate(v));
        }
    }
}
