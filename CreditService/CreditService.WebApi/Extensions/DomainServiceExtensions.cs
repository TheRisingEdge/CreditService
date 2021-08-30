using CreditService.Domain.CreditDecisionMaker;
using Microsoft.Extensions.DependencyInjection;

namespace CreditService.WebApi.Extensions
{
    public static class DomainServiceExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services) {
            return services.AddSingleton<CreditDecisionMaker>();
        }
    }
}
