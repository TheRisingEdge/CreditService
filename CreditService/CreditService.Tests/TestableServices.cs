using Microsoft.Extensions.DependencyInjection;
using CreditService.Application;
using CreditService.WebApi.Controllers;
using CreditService.WebApi.Extensions;
using MediatR;

namespace CreditService.Tests
{
    public class TestableServices
    {
        public ServiceProvider Provider { get; }

        public TestableServices()
        {
            var services = new ServiceCollection();

            services
                .AddDomainServices()
                .AddMediatR(CreditServiceApplication.Assembly)
                .AddScoped<CreditDecisionController>();

            Provider = services.BuildServiceProvider();
        }
    }
}
