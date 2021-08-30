using CreditService.Application.ApplyForCredit;
using CreditService.WebApi.Controllers;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace CreditService.Tests.IntegrationTests
{
    public class ControllerTests : IClassFixture<TestableServices>, IDisposable
    {
        private readonly IServiceScope _testScope;
        private readonly CreditDecisionController _controller;

        public ControllerTests(TestableServices testableServices)
        {
            _testScope = testableServices.Provider.CreateScope();
            _controller = _testScope.ServiceProvider.GetRequiredService<CreditDecisionController>();
        }

        public void Dispose() => _testScope.Dispose();

        [Theory]
        [InlineData(2000, .03)]
        [InlineData(2200, .03)]
        [InlineData(30000, .04)]
        [InlineData(50000, .05)]
        [InlineData(69000, .06)]
        public async void Should_AcceptCredit_InTheValidRange(int amount, float interest)
        {
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
        public async void Should_RejectCredit_InTheInvalidRange(int amount, int preexistingAmount)
        {
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
