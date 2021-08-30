using CreditService.WebApi;
using Microsoft.Extensions.DependencyInjection;
using CreditService.Application;
using CreditService.WebApi.Controllers;
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
