using CreditService.Domain;
using CreditService.Domain.CreditDecisionMaker;
using FluentAssertions;
using Xunit;

namespace CreditService.Tests.UnitTests
{
    public class CreditDecisionMakerTests
    {
        private readonly CreditDecisionMaker _decisionMaker;

        public CreditDecisionMakerTests()
        {
            _decisionMaker = new CreditDecisionMaker();
        }

        [Theory]
        [InlineData(500, false)]
        [InlineData(1000, false)]
        [InlineData(2000, true)]
        [InlineData(2100, true)]
        [InlineData(5200, true)]
        [InlineData(69000, true)]
        [InlineData(70000, false)]
        [InlineData(100000, false)]
        public void Should_AllowOrRejectDecision(int amount, bool okOrNot)
        {
            var decisionRequest = CreateDecisionRequest(amount, 1, 0);

            var decision = _decisionMaker.MakeDecisionFor(decisionRequest);

            decision.IsSuccess.Should().Be(okOrNot);
        }

        [Theory]
        [InlineData(2000, 300, .03f)]
        [InlineData(19000, 100, .03f)]
        [InlineData(20000, 100, .04f)]
        [InlineData(20000, 1000, .04f)]
        [InlineData(40000, 0, .05f)]
        [InlineData(40000, 1500, .05f)]
        [InlineData(59000, 1500, .06f)]
        [InlineData(59000, 3000, .06f)]
        public void Should_ComputeRate(int amount, int existingDebt, float rate)
        {
            var decisionRequest = CreateDecisionRequest(amount, 1, existingDebt);

            var decision = _decisionMaker.MakeDecisionFor(decisionRequest);

            decision.InterestRate.Value.Should().Be(rate);
        }

        private CreditDecisionRequest CreateDecisionRequest(int amount, int term, int preexisting)
        {
            var creditAmount = CreditAmount.TryCreate(amount).Value;
            var repaymentTerm = RepaymentTerm.TryCreate(term).Value;
            var preexistingAmount = CreditAmount.TryCreate(preexisting).Value;

            return new CreditDecisionRequest(creditAmount, repaymentTerm, preexistingAmount);
        }
    }
}
