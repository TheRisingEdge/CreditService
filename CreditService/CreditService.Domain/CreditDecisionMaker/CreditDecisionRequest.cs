namespace CreditService.Domain
{
    public struct CreditDecisionRequest
    {
        public CreditAmount CreditAmount { get; set; }
        public RepaymentTerm RepaymentTerm { get; set; }
        public CreditAmount PreexistingCreditAmount { get; set; }
        public CreditAmount TotalFutureDebt => PreexistingCreditAmount + CreditAmount;

        public CreditDecisionRequest(
            CreditAmount creditAmount,
            RepaymentTerm repaymentTerm,
            CreditAmount preexistingCreditAmount)
        {
            CreditAmount = creditAmount;
            RepaymentTerm = repaymentTerm;
            PreexistingCreditAmount = preexistingCreditAmount;
        }
    }
}
