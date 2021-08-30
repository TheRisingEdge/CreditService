namespace CreditService.Domain.CreditDecisionMaker
{
    public sealed class CreditDecisionMaker
    {
        public CreditDecision MakeDecisionFor(CreditDecisionRequest request)
        {
            return request.CreditAmount.Value switch
            {
                (< 2000) => CreditDecision.Rejection("Applied credit amount cannot be less than 2000"),
                (>= 2000) and (<= 69000) => CreditDecision.Ok(ComputeInterestRateFor(request.TotalFutureDebt)),
                (> 69000) => CreditDecision.Rejection("Applied credit amount cannot be more than 69000")
            };
        }

        private InterestRate ComputeInterestRateFor(CreditAmount totalFutureDebt)
        {
            var rate = totalFutureDebt.Value switch
            {
                (< 20000) => .03f,
                (>= 20000) and (< 40000) => .04f,
                (>= 40000) and (< 60000) => .05f,
                (>= 60000) => .06f,
            };

            return InterestRate.TryCreate(rate).Value;
        }
    }
}
