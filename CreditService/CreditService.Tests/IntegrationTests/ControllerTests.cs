using CreditService.Application.ApplyForCredit;
using CreditService.WebApi.Controllers;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CreditService.Tests.IntegrationTests
{
    public class ControllerTests : IClassFixture<TestableServices>
    {
        private readonly IServiceScope _testScope;
        private readonly CreditDecisionController _controller;

        public ControllerTests(TestableServices testableServices)
        {
           _testScope = testableServices.Provider.CreateScope();
           _controller = _testScope.ServiceProvider.GetRequiredService<CreditDecisionController>();
        }

        [Theory]
        [InlineData(2000, 0.3)]
        [InlineData(2200, 0.3)]
        [InlineData(30000, 0.4)]
        [InlineData(50000, 0.5)]
        [InlineData(69000, 0.6)]
        public async void Should_AcceptCredit_InTheValidRange(int amount, float interest) {
            var request = new ApplyForCreditRequest();
            request.CreditAmount = amount;
            request.RepaymentTerm = 1;

            var decision = await _controller.Get(request)
                .ExpectingOK<ApplyForCreditResponse>();

            decision.IsSuccessful.Should().BeTrue();
            decision.InterestRate.Should().Be(interest);
            decision.RejectionReason.Should().BeNull();
        }

        [Theory]
        [InlineData(1000, 100)]
        [InlineData(69001, 1)]
        public async void Should_RejectCredit_InTheInvalidRange(int amount, int preexistingAmount) {
            var request = new ApplyForCreditRequest();
            request.CreditAmount = amount;
            request.RepaymentTerm = 1;
            request.PreexistingCreditAmount = preexistingAmount;

            var decision = await _controller.Get(request)
                .ExpectingOK<ApplyForCreditResponse>();

            decision.IsSuccessful.Should().BeFalse();
            decision.InterestRate.Should().NotHaveValue();
            decision.RejectionReason.Should().NotBeNullOrEmpty();
        }
    }
}
